using System;
using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **General Harami** (Japanese for "pregnant") two-candlestick pattern.
    /// This pattern is characterized by the second (current) candle's body being entirely contained 
    /// within the body of the previous (first) candle. It generally signals market indecision 
    /// and a potential reversal or consolidation.
    /// </summary>
    public class Recognizer_Harami : Recognizer
    {
        /// <summary>
        /// Constructor for the Harami Recognizer.
        /// </summary>
        public Recognizer_Harami()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Harami";
        }

        /// <summary>
        /// Overrides the base method to check if the two candlesticks ending at the given index 
        /// form a general Harami pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the second (current) candlestick in the pattern.</param>
        /// <returns>True if the pattern is recognized, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // The Harami pattern requires at least two candlesticks.
            // Check if the current index is valid and ensures there is a previous candle (index > 0).
            if (index <= 0 || index >= list.Count)
                return false;

            // Alias for the previous (first/mother) candlestick.
            var prev = list[index - 1];
            // Alias for the current (second/baby) candlestick.
            var curr = list[index];

            // Determine the body's bottom and top for the previous candle.
            decimal prevMin = Math.Min(prev.Open, prev.Close);
            decimal prevMax = Math.Max(prev.Open, prev.Close);
            // Determine the body's bottom and top for the current candle.
            decimal currMin = Math.Min(curr.Open, curr.Close);
            decimal currMax = Math.Max(curr.Open, curr.Close);

            // Harami Rule: 
            // The body of the current candle must be entirely contained within the body of the previous candle.
            // current body inside previous body
            return currMin >= prevMin && currMax <= prevMax;
        }
    }
}