using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// tiny helper that reads one CSV into a list of candlesticks
    /// </summary>
    public static class StockReader
    {
        // I map columns by header so the order doesn't matter
        public static List<Candlestick> Read(string csvPath)
        {
            if (string.IsNullOrWhiteSpace(csvPath) || !File.Exists(csvPath))
                return new List<Candlestick>();

            var lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0) return new List<Candlestick>();

            // header -> index
            string[] hdr = lines[0].Split(',');
            int iDate = Find(hdr, "date");
            int iOpen = Find(hdr, "open");
            int iHigh = Find(hdr, "high");
            int iLow = Find(hdr, "low");
            int iClose = Find(hdr, "close");
            int iVolume = Find(hdr, "volume");

            if (new[] { iDate, iOpen, iHigh, iLow, iClose, iVolume }.Any(i => i < 0))
                throw new InvalidDataException("Need Date, Open, High, Low, Close, Volume in the header.");

            var outList = new List<Candlestick>();
            var ci = CultureInfo.InvariantCulture;
            string[] fmt = { "M/d/yyyy", "MM/dd/yyyy", "M/d/yy", "yyyy-MM-dd" };

            // loop through the rows and parse what we need
            for (int r = 1; r < lines.Length; r++)
            {
                var row = lines[r];
                if (string.IsNullOrWhiteSpace(row)) continue;

                var p = row.Split(',');
                if (p.Length <= Math.Max(iVolume, iDate)) continue;

                try
                {
                    var date = DateTime.ParseExact(p[iDate].Trim().Trim('"'), fmt, ci, DateTimeStyles.None);
                    var open = Convert.ToDecimal(p[iOpen].Trim().Trim('"'), ci);
                    var high = Convert.ToDecimal(p[iHigh].Trim().Trim('"'), ci);
                    var low = Convert.ToDecimal(p[iLow].Trim().Trim('"'), ci);
                    var close = Convert.ToDecimal(p[iClose].Trim().Trim('"'), ci);

                    // volume might be huge or in 1.23E+08 style
                    var rawV = p[iVolume].Trim().Trim('"');
                    ulong volume = ulong.TryParse(rawV, NumberStyles.Any, ci, out var vOk)
                                   ? vOk
                                   : (ulong)Convert.ToDouble(rawV, ci);

                    outList.Add(new Candlestick
                    {
                        Date = date,
                        Open = open,
                        High = high,
                        Low = low,
                        Close = close,
                        Volume = volume
                    });
                }
                catch
                {
                    // if a row is weird, I just skip it so the app doesn’t crash
                }
            }

            return outList.OrderBy(c => c.Date).ToList();
        }

        // find the header index (case-insensitive, trims quotes)
        private static int Find(string[] hdr, string name)
        {
            for (int i = 0; i < hdr.Length; i++)
            {
                var h = hdr[i].Trim().Trim('"', '\'');
                if (string.Equals(h, name, StringComparison.OrdinalIgnoreCase)) return i;
            }
            return -1;
        }
    }
}
