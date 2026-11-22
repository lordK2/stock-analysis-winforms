using System;
using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bearish Harami** two-candlestick pattern.
    /// This pattern is a bearish reversal signal, typically found in an uptrend. 
    /// It consists of a large bullish candle followed by a smaller bearish candle 
    /// whose body is contained within the first candle's body.
    /// </summary>
    public class Recognizer_HaramiBearish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bearish Harami Recognizer.
        /// </summary>
        public Recognizer_HaramiBearish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bearish Harami";
        }

        /// <summary>
        /// Overrides the base method to check if the two candlesticks ending at the given index 
        /// form a Bearish Harami pattern.
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

            // Bearish Harami Rule 1 & 2 (Color): 
            // The previous candle must be bullish and the current candle must be bearish.
            if (!prev.IsBullish || !curr.IsBearish)
                return false;

            // Determine the body's bottom and top for the previous (bullish) candle.
            decimal prevMin = Math.Min(prev.Open, prev.Close);
            decimal prevMax = Math.Max(prev.Open, prev.Close);
            // Determine the body's bottom and top for the current (bearish) candle.
            decimal currMin = Math.Min(curr.Open, curr.Close);
            decimal currMax = Math.Max(curr.Open, curr.Close);

            // Bearish Harami Rule 3 (Body Containment): 
            // The body of the current (second) candle must be entirely contained 
            // within the body of the previous (first) candle.
            return currMin >= prevMin && currMax <= prevMax;
        }
    }
}