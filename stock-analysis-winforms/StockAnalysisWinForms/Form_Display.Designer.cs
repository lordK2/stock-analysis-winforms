namespace StockAnalysisWinForms
{
    partial class Form_Display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.table_root = new System.Windows.Forms.TableLayoutPanel();
            this.chart_OHLCV = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel_toolbar = new System.Windows.Forms.Panel();
            this.button_Simulate = new System.Windows.Forms.Button();
            this.comboBox_Patterns = new System.Windows.Forms.ComboBox();
            this.textBox_TickerInterval = new System.Windows.Forms.TextBox();
            this.hScrollBar_TickerInterval = new System.Windows.Forms.HScrollBar();
            this.dateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
            this.textBox_EndDate = new System.Windows.Forms.TextBox();
            this.textBox_StartDate = new System.Windows.Forms.TextBox();
            this.button_refresh = new System.Windows.Forms.Button();
            this.button_loadData = new System.Windows.Forms.Button();
            this.openFileDialog_LoadTicker = new System.Windows.Forms.OpenFileDialog();
            this.timer_Ticker = new System.Windows.Forms.Timer(this.components);
            this.table_root.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).BeginInit();
            this.panel_toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // table_root
            // 
            this.table_root.ColumnCount = 1;
            this.table_root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_root.Controls.Add(this.chart_OHLCV, 0, 0);
            this.table_root.Controls.Add(this.panel_toolbar, 0, 1);
            this.table_root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table_root.Location = new System.Drawing.Point(0, 0);
            this.table_root.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.table_root.Name = "table_root";
            this.table_root.RowCount = 2;
            this.table_root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.66666F));
            this.table_root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.333333F));
            this.table_root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.table_root.Size = new System.Drawing.Size(1790, 1103);
            this.table_root.TabIndex = 0;
            // 
            // chart_OHLCV
            // 
            chartArea1.AxisX.LabelStyle.Format = "M/d/yyyy";
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.AlignWithChartArea = "ChartArea_OHLC";
            chartArea2.AxisX.LabelStyle.Format = "M/d/yyyy";
            chartArea2.Name = "ChartArea_Volume";
            this.chart_OHLCV.ChartAreas.Add(chartArea1);
            this.chart_OHLCV.ChartAreas.Add(chartArea2);
            this.chart_OHLCV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_OHLCV.Location = new System.Drawing.Point(6, 6);
            this.chart_OHLCV.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chart_OHLCV.Name = "chart_OHLCV";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceUpColor=Lime, PriceDownColor=Red";
            series1.IsXValueIndexed = true;
            series1.Name = "Series_OHLC";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "High,Low,Open,Close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_Volume";
            series2.IsXValueIndexed = true;
            series2.Name = "Series_Volume";
            series2.XValueMember = "Date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "Volume";
            this.chart_OHLCV.Series.Add(series1);
            this.chart_OHLCV.Series.Add(series2);
            this.chart_OHLCV.Size = new System.Drawing.Size(1778, 1010);
            this.chart_OHLCV.TabIndex = 0;
            this.chart_OHLCV.Text = "chart1";
            // 
            // panel_toolbar
            // 
            this.panel_toolbar.AutoSize = true;
            this.panel_toolbar.Controls.Add(this.button_Simulate);
            this.panel_toolbar.Controls.Add(this.comboBox_Patterns);
            this.panel_toolbar.Controls.Add(this.textBox_TickerInterval);
            this.panel_toolbar.Controls.Add(this.hScrollBar_TickerInterval);
            this.panel_toolbar.Controls.Add(this.dateTimePicker_EndDate);
            this.panel_toolbar.Controls.Add(this.dateTimePicker_StartDate);
            this.panel_toolbar.Controls.Add(this.textBox_EndDate);
            this.panel_toolbar.Controls.Add(this.textBox_StartDate);
            this.panel_toolbar.Controls.Add(this.button_refresh);
            this.panel_toolbar.Controls.Add(this.button_loadData);
            this.panel_toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_toolbar.Location = new System.Drawing.Point(6, 1028);
            this.panel_toolbar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel_toolbar.Name = "panel_toolbar";
            this.panel_toolbar.Size = new System.Drawing.Size(1778, 69);
            this.panel_toolbar.TabIndex = 1;
            // 
            // button_Simulate
            // 
            this.button_Simulate.Location = new System.Drawing.Point(0, 0);
            this.button_Simulate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button_Simulate.Name = "button_Simulate";
            this.button_Simulate.Size = new System.Drawing.Size(118, 44);
            this.button_Simulate.TabIndex = 9;
            this.button_Simulate.Text = "Simulate";
            this.button_Simulate.UseVisualStyleBackColor = true;
            this.button_Simulate.Click += new System.EventHandler(this.button_Simulate_Click);
            // 
            // comboBox_Patterns
            // 
            this.comboBox_Patterns.FormattingEnabled = true;
            this.comboBox_Patterns.Location = new System.Drawing.Point(130, 6);
            this.comboBox_Patterns.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBox_Patterns.Name = "comboBox_Patterns";
            this.comboBox_Patterns.Size = new System.Drawing.Size(214, 33);
            this.comboBox_Patterns.TabIndex = 8;
            // 
            // textBox_TickerInterval
            // 
            this.textBox_TickerInterval.Location = new System.Drawing.Point(380, -4);
            this.textBox_TickerInterval.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox_TickerInterval.Name = "textBox_TickerInterval";
            this.textBox_TickerInterval.Size = new System.Drawing.Size(156, 31);
            this.textBox_TickerInterval.TabIndex = 7;
            // 
            // hScrollBar_TickerInterval
            // 
            this.hScrollBar_TickerInterval.Location = new System.Drawing.Point(370, 33);
            this.hScrollBar_TickerInterval.Maximum = 2000;
            this.hScrollBar_TickerInterval.Minimum = 100;
            this.hScrollBar_TickerInterval.Name = "hScrollBar_TickerInterval";
            this.hScrollBar_TickerInterval.Size = new System.Drawing.Size(166, 23);
            this.hScrollBar_TickerInterval.TabIndex = 6;
            this.hScrollBar_TickerInterval.Value = 500;
            this.hScrollBar_TickerInterval.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_TickerInterval_Scroll);
            // 
            // dateTimePicker_EndDate
            // 
            this.dateTimePicker_EndDate.Location = new System.Drawing.Point(1286, 10);
            this.dateTimePicker_EndDate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dateTimePicker_EndDate.Name = "dateTimePicker_EndDate";
            this.dateTimePicker_EndDate.Size = new System.Drawing.Size(364, 31);
            this.dateTimePicker_EndDate.TabIndex = 5;
            // 
            // dateTimePicker_StartDate
            // 
            this.dateTimePicker_StartDate.Location = new System.Drawing.Point(800, 10);
            this.dateTimePicker_StartDate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dateTimePicker_StartDate.Name = "dateTimePicker_StartDate";
            this.dateTimePicker_StartDate.Size = new System.Drawing.Size(358, 31);
            this.dateTimePicker_StartDate.TabIndex = 4;
            // 
            // textBox_EndDate
            // 
            this.textBox_EndDate.Location = new System.Drawing.Point(1174, 12);
            this.textBox_EndDate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox_EndDate.Name = "textBox_EndDate";
            this.textBox_EndDate.Size = new System.Drawing.Size(96, 31);
            this.textBox_EndDate.TabIndex = 3;
            this.textBox_EndDate.Text = "End Date";
            // 
            // textBox_StartDate
            // 
            this.textBox_StartDate.Location = new System.Drawing.Point(666, 10);
            this.textBox_StartDate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox_StartDate.Name = "textBox_StartDate";
            this.textBox_StartDate.Size = new System.Drawing.Size(118, 31);
            this.textBox_StartDate.TabIndex = 2;
            this.textBox_StartDate.Text = "Start Date";
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(1666, 6);
            this.button_refresh.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(106, 44);
            this.button_refresh.TabIndex = 1;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            // 
            // button_loadData
            // 
            this.button_loadData.Location = new System.Drawing.Point(574, 6);
            this.button_loadData.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button_loadData.Name = "button_loadData";
            this.button_loadData.Size = new System.Drawing.Size(80, 44);
            this.button_loadData.TabIndex = 0;
            this.button_loadData.Text = "Load Tickers";
            this.button_loadData.UseVisualStyleBackColor = true;
            // 
            // openFileDialog_LoadTicker
            // 
            this.openFileDialog_LoadTicker.FileName = "openFileDialog1";
            // 
            // Form_Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1790, 1103);
            this.Controls.Add(this.table_root);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form_Display";
            this.Text = "Form_Display";
            this.table_root.ResumeLayout(false);
            this.table_root.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).EndInit();
            this.panel_toolbar.ResumeLayout(false);
            this.panel_toolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table_root;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_OHLCV;
        private System.Windows.Forms.Panel panel_toolbar;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartDate;
        private System.Windows.Forms.TextBox textBox_EndDate;
        private System.Windows.Forms.TextBox textBox_StartDate;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Button button_loadData;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadTicker;
        private System.Windows.Forms.Timer timer_Ticker;
        private System.Windows.Forms.TextBox textBox_TickerInterval;
        private System.Windows.Forms.HScrollBar hScrollBar_TickerInterval;
        private System.Windows.Forms.Button button_Simulate;
        private System.Windows.Forms.ComboBox comboBox_Patterns;
    }
}