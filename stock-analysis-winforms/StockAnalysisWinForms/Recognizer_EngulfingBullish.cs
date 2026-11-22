using System;
using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bullish Engulfing** two-candlestick pattern.
    /// This is a strong bullish reversal pattern where a large bullish candle 
    /// completely engulfs the body of the previous smaller bearish candle.
    /// </summary>
    public class Recognizer_EngulfingBullish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bullish Engulfing Recognizer.
        /// </summary>
        public Recognizer_EngulfingBullish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bullish Engulfing";
        }

        /// <summary>
        /// Overrides the base method to check if the two candlesticks ending at the given index 
        /// form a Bullish Engulfing pattern.
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

            // Bullish Engulfing Rule 1 & 2: 
            // The previous candle must be bearish and the current (engulfing) candle must be bullish.
            if (!prev.IsBearish || !curr.IsBullish)
                return false;

            // Determine the body's bottom and top for the previous (bearish) candle.
            decimal prevMin = Math.Min(prev.Open, prev.Close);
            decimal prevMax = Math.Max(prev.Open, prev.Close);
            // Determine the body's bottom and top for the current (bullish) candle.
            decimal currMin = Math.Min(curr.Open, curr.Close);
            decimal currMax = Math.Max(curr.Open, curr.Close);

            // Bullish Engulfing Rule 3: 
            // The body of the current (bullish) candle must completely engulf the body of the previous (bearish) candle.
            return currMin < prevMin && currMax > prevMax;
        }
    }
}