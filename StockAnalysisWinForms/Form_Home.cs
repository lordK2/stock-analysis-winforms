using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// tiny “launcher” window: I pick dates + CSVs here,
    /// and it pops open one big chart window per file
    /// </summary>
    public partial class Form_Home : Form
    {
        public Form_Home()
        {
            InitializeComponent();
            InitDefaults();
        }

        /// <summary>
        /// set default dates + dialog folder so I don’t click a million times
        /// </summary>
        private void InitDefaults()
        {
            // start+end set to the range we used in class so testing is quick
            dateTimePicker_StartDate.Value = new DateTime(2023, 5, 25);
            dateTimePicker_EndDate.Value = new DateTime(2023, 7, 25);

            // point OpenFileDialog at ..\..\..\Stock Data (same trick from P1)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string stockDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "Stock Data"));
            openFileDialog_LoadTickers.InitialDirectory = Directory.Exists(stockDir) ? stockDir : baseDir;

            openFileDialog_LoadTickers.Filter =
                "All CSV (*.csv)|*.csv|Daily (*-Day.csv)|*-Day.csv|Weekly (*-Week.csv)|*-Week.csv|Monthly (*-Month.csv)|*-Month.csv|All files (*.*)|*.*";
            openFileDialog_LoadTickers.Multiselect = true;

            // when I click, we open files
            button_loadData.Click += button_loadTickers_Click;
        }

        /// <summary>
        /// pick one or more CSVs and open one display window per file.
        /// all displays use the same date range I chose here.
        /// </summary>
        private void button_loadTickers_Click(object sender, EventArgs e)
        {
            if (openFileDialog_LoadTickers.ShowDialog(this) != DialogResult.OK) return;

            var start = dateTimePicker_StartDate.Value.Date;
            var end = dateTimePicker_EndDate.Value.Date;

            // loop the selected paths and pop a chart window for each one
            foreach (var path in openFileDialog_LoadTickers.FileNames.Where(File.Exists))
            {
                var win = new Form_Display(path, start, end); // pass path + dates
                win.Show(); // not modal, so I can open a bunch
            }
        }
    }
}
