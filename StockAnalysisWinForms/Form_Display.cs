using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Big chart window for ONE CSV. The skinny home form opens one of these per file.
    /// </summary>
    public partial class Form_Display : Form
    {
        // full data from file (I keep this so Refresh doesn't re-read the CSV)
        private List<Candlestick> _all = new List<Candlestick>();

        // filtered view that the chart binds to (BindingList so the chart can observe changes)
        private BindingList<Candlestick> _view = new BindingList<Candlestick>();

        // stash the path so I can show it in the title and debug if I need to
        private string _currentPath = string.Empty;

        // ====================== Project 3 fields ======================

        // All candlesticks currently loaded for this ticker / date range
        private List<Candlestick> _listOfCandlesticks = new List<Candlestick>();

        // Smart versions of the current candlesticks
        private List<SmartCandlestick> _smartCandlesCurrent = new List<SmartCandlestick>();

        // Handles all pattern recognizers
        private RecognizerManager _recognizerManager = new RecognizerManager();

        // Ticker playback state (for simulate)
        private bool _tickerRunning = false;
        private int _tickerIndex = 0;

        // Sub-list that grows as we simulate the ticker revealing candles
        private BindingSource _bindingSourceTicker = new BindingSource();
        private List<Candlestick> _tickerList = new List<Candlestick>();


        /// <summary>
        /// Designer needs this default ctor. Also useful to open the form empty for testing.
        /// </summary>
        public Form_Display()
        {
            InitializeComponent();     // builds the controls the designer created

            // start with an empty pattern list; it will be filled AFTER the first simulation
            comboBox_Patterns.Items.Clear();
            comboBox_Patterns.SelectedIndex = -1;
            comboBox_Patterns.Text = "";

            comboBox_Patterns.SelectedIndexChanged += comboBox_Patterns_SelectedIndexChanged;
            timer_Ticker.Tick += timer_Ticker_Tick;
            button_Simulate.Click += button_Simulate_Click;


            // keep textbox + scrollbar in sync from the start
            textBox_TickerInterval.Text = hScrollBar_TickerInterval.Value.ToString();
            textBox_TickerInterval.TextChanged += textBox_TickerInterval_TextChanged;


            WireUpUi();                // hook events + set chart props in one place
        }

        /// <summary>
        /// Normal “launch with data” path: skinny form passes file path + date range.
        /// </summary>
        public Form_Display(string csvPath, DateTime start, DateTime end) : this()
        {
            // copy the range from the skinny launcher so both windows agree
            dateTimePicker_StartDate.Value = start;
            dateTimePicker_EndDate.Value = end;

            // read the CSV once into memory, then do the filter/display pass
            LoadFromPath(csvPath);
            ApplyFilterAndShow(start, end);
        }

        /// <summary>
        /// One-time setup for this window: wire up events, set chart settings, and dialog filter.
        /// </summary>
        private void WireUpUi()
        {
            // hook buttons so clicks call our methods
            button_loadData.Click += button_loadData_Click;
            button_refresh.Click += button_refresh_Click;

            // file picker: single file here (multi-select lives in the skinny form)
            openFileDialog_LoadTicker.Filter =
                "All CSV (*.csv)|*.csv|Daily (*-Day.csv)|*-Day.csv|Weekly (*-Week.csv)|*-Week.csv|Monthly (*-Month.csv)|*-Month.csv|All files (*.*)|*.*";
            openFileDialog_LoadTicker.Multiselect = false;

            // point the chart’s DataSource at the view list we control
            chart_OHLCV.DataSource = _view;

            // spec says: remove legend to save space on the right
            if (chart_OHLCV.Legends.Count > 0)
                chart_OHLCV.Legends.Clear();

            // format X axis as dates; series use IsXValueIndexed to hide weekend gaps
            chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisX.LabelStyle.Format = "M/d/yyyy";
            chart_OHLCV.ChartAreas["ChartArea_Volume"].AxisX.LabelStyle.Format = "M/d/yyyy";

            // double-check series properties (this is harmless if the designer already set them)
            var sOHLC = chart_OHLCV.Series.FindByName("Series_OHLC");
            if (sOHLC != null)
            {
                sOHLC.ChartType = SeriesChartType.Candlestick;   // candles for OHLC
                sOHLC.IsXValueIndexed = true;                         // no gaps for weekends/holidays
                sOHLC.XValueMember = "Date";                        // bind X to date
                sOHLC.XValueType = ChartValueType.DateTime;       // tell chart it’s dates
                sOHLC.YValuesPerPoint = 4;                            // High/Low/Open/Close
                sOHLC.YValueMembers = "High,Low,Open,Close";         // actual mapping order
                sOHLC["PriceUpColor"] = "Lime";                       // green up candle
                sOHLC["PriceDownColor"] = "Red";                      // red down candle
                sOHLC.ChartArea = "ChartArea_OHLC";              // top chart area
            }

            var sVol = chart_OHLCV.Series.FindByName("Series_Volume");
            if (sVol != null)
            {
                sVol.ChartType = SeriesChartType.Column;       // bars for volume
                sVol.IsXValueIndexed = true;                         // align bars w/ candles
                sVol.XValueMember = "Date";                       // same X as candles
                sVol.XValueType = ChartValueType.DateTime;      // dates again
                sVol.YValueMembers = "Volume";                     // bind to volume column
                sVol.ChartArea = "ChartArea_Volume";           // bottom chart area
            }
        }

        /// <summary>
        /// Load button inside this window (nice for testing this form by itself).
        /// </summary>
        private void button_loadData_Click(object sender, EventArgs e)
        {
            if (openFileDialog_LoadTicker.ShowDialog(this) != DialogResult.OK)
                return;                             // user canceled, do nothing

            LoadFromPath(openFileDialog_LoadTicker.FileName);         // read CSV once
            ApplyFilterAndShow(dateTimePicker_StartDate.Value.Date,   // then apply filter
                               dateTimePicker_EndDate.Value.Date);
        }

        private void BuildSmartCandlesFromList(List<Candlestick> src)
        {
            _smartCandlesCurrent.Clear();
            foreach (var cs in src)
                _smartCandlesCurrent.Add(new SmartCandlestick(cs));
        }

        private void RunAllRecognizers()
        {
            if (_smartCandlesCurrent.Count == 0)
                return;

            _recognizerManager.RunAll(_smartCandlesCurrent);
        }


        /// <summary>
        /// Refresh just re-applies the date filter to the in-memory list (no file I/O).
        /// </summary>
        private void button_refresh_Click(object sender, EventArgs e)
        {
            ResetPatternUI();
            chart_OHLCV.Annotations.Clear();
            ApplyFilterAndShow(dateTimePicker_StartDate.Value.Date,   // grab current pickers
                               dateTimePicker_EndDate.Value.Date);    // and re-run filter
        }


        /// <summary>
        /// Read the CSV once, set picker bounds based on file span, and set the window title.
        ///  We do NOT render here. The caller will run ApplyFilterAndShow(...) after
        /// doing date validation so we don't accidentally draw with bad ranges.
        /// </summary>
        private void LoadFromPath(string path)
        {

            _currentPath = path ?? string.Empty;              // remember path for title/debug

            // read the whole file into memory once; keep a bindable view list
            _all = ReadCandlesticksFromFile(_currentPath);
            _view = new BindingList<Candlestick>(_all.ToList());

            ResetPatternUI();


            // point the chart at our view list, but DON'T DataBind yet (we bind after validation)
            chart_OHLCV.DataSource = _view;

            // set safe bounds for the pickers
            if (_all != null && _all.Count > 0)
            {
                var min = _all.Min(c => c.Date).Date;
                var max = _all.Max(c => c.Date).Date;

                dateTimePicker_StartDate.MinDate = new DateTime(1900, 1, 1);
                dateTimePicker_StartDate.MaxDate = new DateTime(2100, 12, 31);
                dateTimePicker_EndDate.MinDate = new DateTime(1900, 1, 1);
                dateTimePicker_EndDate.MaxDate = new DateTime(2100, 12, 31);
            }

            // helpful caption like "ABBV-Day" so I know what this window shows
            this.Text = GetSymbolPeriodFromPath(_currentPath);


        }


        /// <summary>
        /// Validates + displays the chosen range. If start > end → popup + stop (no swap).
        /// If range is outside the file span → ClampToData handles the warning and clamping.
        /// Then we filter → normalize → bind → update title.
        /// </summary>
        private void ApplyFilterAndShow(DateTime start, DateTime end)
        {
            // if student fat-fingers the dates (start > end), just tell them and bail
            if (!ValidateStartBeforeEnd(start, end))
                return; // no rebind, no chart changes

            chart_OHLCV.Annotations.Clear();
            _recognizerManager.Recognizers.ForEach(r => r.Reset());



            // bring the dates inside the file span (this shows a heads-up if we had to clamp)
            (start, end) = ClampToData(start, end);

            // slice the full list down to the requested window (oldest → newest)
            var filtered = _all
                .Where(c => c.Date >= start && c.Date <= end)
                .OrderBy(c => c.Date)
                .ToList();

            _listOfCandlesticks = filtered;   // <-- make simulate / recognizers use this range

            _view = new BindingList<Candlestick>(filtered);  // swap in the new view

            //this is the list used for simulate + recognizers
            _listOfCandlesticks = filtered;

            NormalizeChart(_view.ToList());                  // set Y axis nicely w/ ~2% pad

            chart_OHLCV.DataSource = _view;                  // point chart at the new view
            chart_OHLCV.DataBind();                          // push data into the chart

            UpdateChartTitle(_currentPath, start, end);      // set the two-line title
        }

        /// <summary>
        /// Normalize the OHLC chart’s Y axis to fit the visible candles (+2% padding).
        /// Empty list means I let the chart auto-scale.
        /// </summary>
        private void NormalizeChart(List<Candlestick> shown)
        {
            var area = chart_OHLCV.ChartAreas["ChartArea_OHLC"];  // top chart area for OHLC
            if (shown == null || shown.Count == 0)
            {
                area.AxisY.Minimum = double.NaN;   // NaN tells chart “you pick the scale”
                area.AxisY.Maximum = double.NaN;
                return;                            // nothing else to do
            }

            decimal lo = shown.Min(c => c.Low);   // lowest price in the window
            decimal hi = shown.Max(c => c.High);  // highest price in the window
            decimal pad = (hi - lo) * 0.02m;       // tiny breathing room so candles aren’t glued

            area.AxisY.Minimum = (double)(lo - pad);
            area.AxisY.Maximum = (double)(hi + pad);
        }

        /// <summary>
        /// Two-line title: top = "SYMBOL-Period", bottom = "M/d/yyyy – M/d/yyyy".
        /// </summary>
        private void UpdateChartTitle(string filePath, DateTime start, DateTime end)
        {
            string label = GetSymbolPeriodFromPath(filePath);                     // ex: ABBV-Day
            string s = start.ToString("M/d/yyyy", CultureInfo.InvariantCulture);  // left date
            string e = end.ToString("M/d/yyyy", CultureInfo.InvariantCulture);    // right date

            chart_OHLCV.Titles.Clear();                                          // nuke old titles
            chart_OHLCV.Titles.Add(label + Environment.NewLine + s + " – " + e); // add fresh one
        }

        /// <summary>
        /// Pull the base filename without extension so we can show “ABBV-Day”.
        /// </summary>
        private static string GetSymbolPeriodFromPath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return string.Empty; // nothing to show
            try { return Path.GetFileNameWithoutExtension(filePath) ?? string.Empty; }
            catch { return string.Empty; }                                 // super defensive
        }

        /// <summary>
        /// Read a CSV into a list (header-driven so column order doesn’t matter).
        /// Returns oldest → newest by Date.
        /// </summary>
        private static List<Candlestick> ReadCandlesticksFromFile(string csvPath)
        {
            var list = new List<Candlestick>();                       // result list
            if (string.IsNullOrWhiteSpace(csvPath) || !File.Exists(csvPath)) return list;

            var lines = File.ReadAllLines(csvPath);                    // grab all lines
            if (lines.Length == 0) return list;                        // empty file guard

            string[] header = lines[0].Split(',');                     // header row
            int idxDate = FindIndex(header, "date");
            int idxOpen = FindIndex(header, "open");
            int idxHigh = FindIndex(header, "high");
            int idxLow = FindIndex(header, "low");
            int idxClose = FindIndex(header, "close");
            int idxVolume = FindIndex(header, "volume");

            if (idxDate < 0 || idxOpen < 0 || idxHigh < 0 || idxLow < 0 || idxClose < 0 || idxVolume < 0)
                throw new InvalidDataException("CSV is missing one of: Date, Open, High, Low, Close, Volume.");

            // walk each data row and try to parse it
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i]; if (string.IsNullOrWhiteSpace(line)) continue;

                var p = line.Split(',');                                             // split fields
                if (p.Length <= Math.Max(idxVolume, Math.Max(idxClose, idxDate)))    // malformed guard
                    continue;

                if (TryParseRow(p, idxDate, idxOpen, idxHigh, idxLow, idxClose, idxVolume, out var c))
                    list.Add(c);                                                     // only add valid rows
            }

            return list.OrderBy(c => c.Date).ToList();                               // oldest → newest
        }

        /// <summary>
        /// ✅ New: Just validate order. If start > end → show popup and DON’T swap.
        /// Returns true if OK, false if invalid.
        /// </summary>
        private bool ValidateStartBeforeEnd(DateTime start, DateTime end)
        {
            if (start <= end) return true;                    // fine, keep going

            MessageBox.Show("Start date can’t be after End date. Please fix the dates and try again.",
                "Invalid date range", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return false;                                     // caller should bail out
        }

        /// <summary>
        /// Get the min/max date inside the currently loaded file. If no data, return (Max, Min).
        /// </summary>
        private (DateTime min, DateTime max) GetDataSpan()
        {
            if (_all == null || _all.Count == 0) return (DateTime.MaxValue, DateTime.MinValue);
            var min = _all.Min(c => c.Date).Date;             // earliest row
            var max = _all.Max(c => c.Date).Date;             // latest row
            return (min, max);
        }

        /// <summary>
        /// If [start,end] is outside the file’s span, I warn and clamp it back inside.
        /// I also write the clamped values back to the pickers so the UI matches the data.
        /// </summary>
        private (DateTime start, DateTime end) ClampToData(DateTime start, DateTime end)
        {
            var (min, max) = GetDataSpan();                   // get file range
            if (min == DateTime.MaxValue) return (start, end); // nothing loaded yet → do nothing

            // no overlap at all → snap to full span (and tell the user why)
            if (end < min || start > max)
            {
                MessageBox.Show(
                    $"No data in that range.\nAvailable: {min:M/d/yyyy} – {max:M/d/yyyy}",
                    "Out of range", MessageBoxButtons.OK, MessageBoxIcon.Information);

                start = min; end = max;                       // pick full file span
            }
            else
            {
                // partial overlap → clamp the edges (and let them know we adjusted)
                var origStart = start; var origEnd = end;
                if (start < min) start = min;
                if (end > max) end = max;

                if (start != origStart || end != origEnd)
                {
                    MessageBox.Show(
                        $"Some dates were outside the available range.\nClamped to: {start:M/d/yyyy} – {end:M/d/yyyy}",
                        "Adjusted dates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // reflect back to the UI so the pickers show the truth
            dateTimePicker_StartDate.Value = start;
            dateTimePicker_EndDate.Value = end;
            return (start, end);
        }

        /// <summary>
        /// Case-insensitive header lookup (also trims quotes, because CSVs love quotes).
        /// </summary>
        private static int FindIndex(string[] hdr, string name)
        {
            for (int i = 0; i < hdr.Length; i++)
            {
                var h = hdr[i].Trim().Trim('"', '\'');             // clean the field first
                if (string.Equals(h, name, StringComparison.OrdinalIgnoreCase))
                    return i;                                      // found a match
            }
            return -1;                                             // not found
        }

        /// <summary>
        /// Parse a single CSV row. Handles yyyy-MM-dd and M/d/yyyy. Volume can be huge or in sci-notation.
        /// </summary>
        private static bool TryParseRow(
            string[] p, int idxDate, int idxOpen, int idxHigh, int idxLow, int idxClose, int idxVolume,
            out Candlestick cs)
        {
            cs = null;                                             // start with “no row”
            try
            {
                var ci = CultureInfo.InvariantCulture;             // consistent parsing
                string[] dateFormats = { "M/d/yyyy", "MM/dd/yyyy", "M/d/yy", "yyyy-MM-dd" };

                DateTime date = DateTime.ParseExact(p[idxDate].Trim().Trim('"'), dateFormats, ci, DateTimeStyles.None);
                decimal open = Convert.ToDecimal(p[idxOpen].Trim().Trim('"'), ci);
                decimal high = Convert.ToDecimal(p[idxHigh].Trim().Trim('"'), ci);
                decimal low = Convert.ToDecimal(p[idxLow].Trim().Trim('"'), ci);
                decimal close = Convert.ToDecimal(p[idxClose].Trim().Trim('"'), ci);

                string vRaw = p[idxVolume].Trim().Trim('"');       // volume can be 123 or 1.23E+09
                ulong volume;
                if (!ulong.TryParse(vRaw, NumberStyles.Any, ci, out volume))
                    volume = (ulong)Convert.ToDouble(vRaw, ci);     // sci-notation fallback

                cs = new Candlestick { Date = date, Open = open, High = high, Low = low, Close = close, Volume = volume };
                return true;                                        // row was good
            }
            catch
            {
                return false;                                       // bad row → skip it
            }
        }

        private void hScrollBar_TickerInterval_Scroll(object sender, ScrollEventArgs e)
        {
            // get the current scrollbar value (100 – 2000)
            int value = hScrollBar_TickerInterval.Value;

            // show it in the textbox
            textBox_TickerInterval.Text = value.ToString();

            // and also update the ticker timer speed
            timer_Ticker.Interval = value;
        }


        private void textBox_TickerInterval_TextChanged(object sender, EventArgs e)
        {
            int value;

            // Try to parse whatever is in the textbox
            if (int.TryParse(textBox_TickerInterval.Text, out value))
            {
                // clamp between scrollbar min and max
                if (value < hScrollBar_TickerInterval.Minimum)
                    value = hScrollBar_TickerInterval.Minimum;

                if (value > hScrollBar_TickerInterval.Maximum)
                    value = hScrollBar_TickerInterval.Maximum;

                // reflect clamped value back into UI + timer
                hScrollBar_TickerInterval.Value = value;
                timer_Ticker.Interval = value;
            }
            // if parse fails (letters etc.), we just do nothing and wait for valid input
        }


        // ========================= SIMULATION BUTTON =========================
        // This starts the “real-time” playback where candles appear one by one.
        private void button_Simulate_Click(object sender, EventArgs e)
        {
            // If the user didn’t load any candles yet, we can’t simulate anything.
            if (_listOfCandlesticks.Count == 0)
            {
                MessageBox.Show("Load data first.");
                return;
            }

            // Reset recognizers & pattern UI for a fresh simulation.
            comboBox_Patterns.Items.Clear();
            comboBox_Patterns.SelectedIndex = -1;
            comboBox_Patterns.Text = "";
            chart_OHLCV.Annotations.Clear();
            _recognizerManager.Recognizers.ForEach(r => r.Reset());


            // Reset everything for a fresh simulation.
            _tickerList.Clear();   // the list that grows one candle at a time
            _tickerIndex = 0;      // start revealing from the very first candle
            _tickerRunning = true; // tells the timer we are in simulation mode

            // Clean the chart so it shows nothing at the beginning.
            chart_OHLCV.Series["Series_OHLC"].Points.Clear();
            chart_OHLCV.Series["Series_Volume"].Points.Clear();
            chart_OHLCV.Annotations.Clear();   // remove any old arrows

            // Match the timer speed with the scrollbar value.
            timer_Ticker.Interval = hScrollBar_TickerInterval.Value;

            // Start the timer → candles begin appearing one by one.
            timer_Ticker.Start();
        }

        private void ResetPatternUI()
        {
            // Clear arrows
            chart_OHLCV.Annotations.Clear();

            // Clear patterns in ComboBox
            comboBox_Patterns.Items.Clear();
            comboBox_Patterns.SelectedIndex = -1;
            comboBox_Patterns.Text = "";

            // Reset recognizer indices
            foreach (var r in _recognizerManager.Recognizers)
                r.Reset();   // this clears r.Indices
        }




        // ========================= SIMULATION TICK =========================
        // Runs every X milliseconds (based on scrollbar). Adds one candle at a time.
        private void timer_Ticker_Tick(object sender, EventArgs e)
        {
            // If for some reason simulation stopped, do nothing.
            if (!_tickerRunning)
                return;

            // Still have candles left to reveal?
            if (_tickerIndex < _listOfCandlesticks.Count)
            {
                // Reveal the next candle on screen.
                _tickerList.Add(_listOfCandlesticks[_tickerIndex]);
                _tickerIndex++;

                // Draw ONLY what we have revealed so far.
                DisplayCandlesticks(_tickerList);
            }
            else
            {
                // No more candles → stop the animation.
                timer_Ticker.Stop();
                _tickerRunning = false;

                // Now show the FULL dataset (final ending snapshot).
                DisplayCandlesticks(_listOfCandlesticks);

                // Build smart candlesticks for pattern recognition.
                BuildSmartCandlesFromList(_listOfCandlesticks);
                RunAllRecognizers();

                // Now that we have results, fill the ComboBox with all patterns.
                comboBox_Patterns.Items.Clear();
                comboBox_Patterns.Text = "";
                foreach (var r in _recognizerManager.Recognizers)
                    comboBox_Patterns.Items.Add(r.Name);

                comboBox_Patterns.SelectedIndex = -1;
            }

        }



        // ========================= DRAW CANDLESTICKS =========================
        // This method draws BOTH the OHLC candlesticks and the volume bars.
        // Used during simulation (partial list) AND after it ends (full list).
        private void DisplayCandlesticks(List<Candlestick> list)
        {
            var sOHLC = chart_OHLCV.Series["Series_OHLC"];
            var sVol = chart_OHLCV.Series["Series_Volume"];

            // Clean old points before drawing new ones.
            sOHLC.Points.Clear();
            sVol.Points.Clear();

            // Plot every candle one by one.
            foreach (var c in list)
            {
                // Add OHLC point — the chart library expects “High” as the main value,
                // but we override YValues to plug in High/Low/Open/Close manually.
                int pointIndex = sOHLC.Points.AddXY(c.Date, c.High);
                var p = sOHLC.Points[pointIndex];

                p.YValues = new double[]
                {
            (double)c.High,
            (double)c.Low,
            (double)c.Open,
            (double)c.Close
                };

                // Add the volume bar for the same date.
                sVol.Points.AddXY(c.Date, (double)c.Volume);
            }

            // Force the chart to redraw.
            chart_OHLCV.Invalidate();
            chart_OHLCV.Update();
        }



        // ========================= COMBOBOX PATTERN SELECTION =========================
        // Runs whenever the user changes the selected pattern in the dropdown.
        private void comboBox_Patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Do NOT annotate while simulation is running (arrows will jump all over).
            if (_tickerRunning)
                return;

            // Highlight only the pattern user selected.
            AnnotateSelectedPattern();
        }



        // ========================= DRAW ARROWS ABOVE MATCHING CANDLES =========================
        // After recognizers find pattern locations, this draws the arrows over those candles.
        private void AnnotateSelectedPattern()
        {
            // If nothing selected, nothing to draw.
            if (comboBox_Patterns.SelectedIndex < 0)
                return;

            // Pick the recognizer that matches the combo box entry.
            var r = _recognizerManager.Recognizers[comboBox_Patterns.SelectedIndex];

            // If this pattern doesn't exist for this dataset, clear arrows & notify.
            if (r.Indices.Count == 0)
            {
                chart_OHLCV.Annotations.Clear();
                chart_OHLCV.Update();
                MessageBox.Show($"No occurrences of \"{r.Name}\" found.");
                return;
            }

            // Remove any old arrows before drawing new ones.
            chart_OHLCV.Annotations.Clear();

            var area = chart_OHLCV.ChartAreas["ChartArea_OHLC"];
            var series = chart_OHLCV.Series["Series_OHLC"];

            // Loop through every candle index where the pattern appears.
            foreach (int i in r.Indices)
            {
                // Safety check.
                if (i < 0 || i >= series.Points.Count)
                    continue;

                // Grab the actual candlestick DataPoint.
                var dp = series.Points[i];

                // Create the downward arrow that sits above the candle.
                ArrowAnnotation arrow = new ArrowAnnotation
                {
                    AxisX = area.AxisX,
                    AxisY = area.AxisY,
                    ClipToChartArea = "ChartArea_OHLC",
                    AnchorDataPoint = dp,            // THIS makes arrow follow the candle
                    LineColor = Color.Black,
                    LineWidth = 2,
                    ArrowSize = 7,
                    Height = -5,                     // negative = arrow above candle
                    Width = 0                        // straight vertical
                };

                chart_OHLCV.Annotations.Add(arrow);
            }

            // Redraw chart with new annotations.
            chart_OHLCV.Invalidate();
            chart_OHLCV.Update();
        }


    }
}
