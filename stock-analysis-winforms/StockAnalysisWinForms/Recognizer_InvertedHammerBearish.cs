using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bearish Inverted Hammer** candlestick pattern.
    /// This pattern is commonly known as a **Shooting Star**. It is a single candle bearish reversal signal, 
    /// characterized by a small bearish body near the low with a long upper shadow (wick). 
    /// It is typically found at the top of an uptrend.
    /// </summary>
    public class Recognizer_InvertedHammerBearish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bearish Inverted Hammer Recognizer.
        /// </summary>
        public Recognizer_InvertedHammerBearish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bearish Inverted Hammer";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Bearish Inverted Hammer (Shooting Star) pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Bearish Inverted Hammer, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsBearishInvertedHammer' is true.
            return index >= 0 && index < list.Count && list[index].IsBearishInvertedHammer;
        }
    }
}