using System;
using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bullish Harami** two-candlestick pattern.
    /// This pattern is a bullish reversal signal, typically found in a downtrend. 
    /// It consists of a large bearish candle followed by a smaller bullish candle 
    /// whose body is contained within the first candle's body.
    /// </summary>
    public class Recognizer_HaramiBullish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bullish Harami Recognizer.
        /// </summary>
        public Recognizer_HaramiBullish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bullish Harami";
        }

        /// <summary>
        /// Overrides the base method to check if the two candlesticks ending at the given index 
        /// form a Bullish Harami pattern.
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

            // Bullish Harami Rule 1 & 2 (Color): 
            // The previous candle must be bearish and the current candle must be bullish.
            if (!prev.IsBearish || !curr.IsBullish)
                return false;

            // Determine the body's bottom and top for the previous (bearish) candle.
            decimal prevMin = Math.Min(prev.Open, prev.Close);
            decimal prevMax = Math.Max(prev.Open, prev.Close);
            // Determine the body's bottom and top for the current (bullish) candle.
            decimal currMin = Math.Min(curr.Open, curr.Close);
            decimal currMax = Math.Max(curr.Open, curr.Close);

            // Bullish Harami Rule 3 (Body Containment): 
            // The body of the current (second) candle must be entirely contained 
            // within the body of the previous (first) candle.
            return currMin >= prevMin && currMax <= prevMax;
        }
    }
}