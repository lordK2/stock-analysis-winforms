using System;
using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bearish Engulfing** two-candlestick pattern.
    /// This is a strong bearish reversal pattern where a large bearish candle 
    /// completely engulfs the body of the previous smaller bullish candle.
    /// </summary>
    public class Recognizer_EngulfingBearish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bearish Engulfing Recognizer.
        /// </summary>
        public Recognizer_EngulfingBearish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bearish Engulfing";
        }

        /// <summary>
        /// Overrides the base method to check if the two candlesticks ending at the given index 
        /// form a Bearish Engulfing pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the second (current) candlestick in the pattern.</param>
        /// <returns>True if the pattern is recognized, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // The pattern requires at least two candlesticks.
            // Check if the current index is valid and ensures there is a previous candle (index > 0).
            if (index <= 0 || index >= list.Count)
                return false;

            // Alias for the previous (first) candlestick.
            var prev = list[index - 1];
            // Alias for the current (second) candlestick.
            var curr = list[index];

            // Bearish Engulfing Rule 1 & 2: 
            // The previous candle must be bullish and the current (engulfing) candle must be bearish.
            if (!prev.IsBullish || !curr.IsBearish)
                return false;

            // Determine the body's bottom and top for the previous (bullish) candle.
            decimal prevMin = Math.Min(prev.Open, prev.Close);
            decimal prevMax = Math.Max(prev.Open, prev.Close);
            // Determine the body's bottom and top for the current (bearish) candle.
            decimal currMin = Math.Min(curr.Open, curr.Close);
            decimal currMax = Math.Max(curr.Open, curr.Close);

            // Bearish Engulfing Rule 3: 
            // The body of the current (bearish) candle must completely engulf the body of the previous (bullish) candle.
            return currMin < prevMin && currMax > prevMax;
        }
    }
}