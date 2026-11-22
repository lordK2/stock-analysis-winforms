using System;
using System.Windows.Forms;

namespace StockAnalysisWinForms
{
    internal static class Program
    {
        /// <summary>
        /// App entry point. Boots WinForms and opens the main form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();                      // use modern control styles
            Application.SetCompatibleTextRenderingDefault(false);  // stick to GDI+ text (default)
            Application.Run(new Form_Home());                         // launch the app on Form1
        }
    }
}
