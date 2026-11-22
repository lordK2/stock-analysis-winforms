using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bullish Hammer** candlestick pattern.
    /// This pattern is a strong bullish reversal signal, characterized by a small bullish body 
    /// near the high with a long lower shadow (wick). It is typically found at the bottom of a downtrend.
    /// </summary>
    public class Recognizer_HammerBullish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bullish Hammer Recognizer.
        /// </summary>
        public Recognizer_HammerBullish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bullish Hammer";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Bullish Hammer pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Bullish Hammer, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsBullishHammer' is true.
            return index >= 0 && index < list.Count && list[index].IsBullishHammer;
        }
    }
}