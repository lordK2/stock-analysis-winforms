using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the Doji candlestick pattern.
    /// A Doji is a single candlestick that signals market indecision.
    /// </summary>
    public class Recognizer_Doji : Recognizer
    {
        /// <summary>
        /// Constructor for the Doji Recognizer.
        /// </summary>
        public Recognizer_Doji()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Doji";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Doji pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Doji, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsDoji' is true.
            return index >= 0 && index < list.Count && list[index].IsDoji;
        }
    }
}