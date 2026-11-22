using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Hammer** candlestick pattern.
    /// The Hammer is a bullish reversal pattern, characterized by a small body near the high 
    /// with a long lower shadow (wick) that is at least twice the size of the body.
    /// </summary>
    public class Recognizer_Hammer : Recognizer
    {
        /// <summary>
        /// Constructor for the Hammer Recognizer.
        /// </summary>
        public Recognizer_Hammer()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Hammer";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Hammer pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Hammer, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsHammer' is true.
            return index >= 0 && index < list.Count && list[index].IsHammer;
        }
    }
}