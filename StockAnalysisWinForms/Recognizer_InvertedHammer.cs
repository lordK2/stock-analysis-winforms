using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Inverted Hammer** candlestick pattern.
    /// This pattern is a single candle bullish reversal signal, characterized by a small body 
    /// near the low with a long upper shadow (wick). It is typically found at the bottom of a downtrend.
    /// </summary>
    public class Recognizer_InvertedHammer : Recognizer
    {
        /// <summary>
        /// Constructor for the Inverted Hammer Recognizer.
        /// </summary>
        public Recognizer_InvertedHammer()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Inverted Hammer";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as an Inverted Hammer pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is an Inverted Hammer, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsInvertedHammer' is true.
            return index >= 0 && index < list.Count && list[index].IsInvertedHammer;
        }
    }
}