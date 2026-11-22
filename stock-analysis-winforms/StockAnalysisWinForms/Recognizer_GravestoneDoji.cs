using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Gravestone Doji** candlestick pattern.
    /// This pattern is characterized by the open, close, and low prices being virtually the same, 
    /// with a long upper shadow (wick). It often signals a potential bearish reversal.
    /// </summary>
    public class Recognizer_GravestoneDoji : Recognizer
    {
        /// <summary>
        /// Constructor for the Gravestone Doji Recognizer.
        /// </summary>
        public Recognizer_GravestoneDoji()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Gravestone Doji";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Gravestone Doji pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Gravestone Doji, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsGravestoneDoji' is true.
            return index >= 0 && index < list.Count && list[index].IsGravestoneDoji;
        }
    }
}