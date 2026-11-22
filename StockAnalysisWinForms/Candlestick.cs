using System;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// One OHLCV row we bind to the grid and the chart.
    /// Kept minimal on purpose (matches what the form expects).
    /// </summary>
    public class Candlestick
    {
        /// <summary>Date for this bar (X-axis on the chart).</summary>
        public DateTime Date { get; set; }

        /// <summary>Open price.</summary>
        public decimal Open { get; set; }

        /// <summary>High price.</summary>
        public decimal High { get; set; }

        /// <summary>Low price.</summary>
        public decimal Low { get; set; }

        /// <summary>Close price.</summary>
        public decimal Close { get; set; }

        /// <summary>Trade volume (can be large; using ulong).</summary>
        public ulong Volume { get; set; }

        /// <summary>
        /// Quick flag the UI can use to color the candle.
        /// True = green (close >= open); False = red.
        /// </summary>
        public bool IsUp => Close >= Open; // tiny convenience
    }
}
