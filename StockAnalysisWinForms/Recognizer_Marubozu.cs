using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Marubozu** candlestick pattern.
    /// A Marubozu is a strong, single-candle pattern characterized by the absence of wicks 
    /// (or very tiny ones), meaning the open and close are also the high and low, 
    /// indicating strong, decisive buying or selling pressure throughout the period.
    /// </summary>
    public class Recognizer_Marubozu : Recognizer
    {
        /// <summary>
        /// Constructor for the Marubozu Recognizer.
        /// </summary>
        public Recognizer_Marubozu()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Marubozu";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Marubozu pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Marubozu, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsMarubozu' is true.
            return index >= 0 && index < list.Count && list[index].IsMarubozu;
        }
    }
}