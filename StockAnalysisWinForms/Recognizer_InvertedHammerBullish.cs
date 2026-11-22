using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bullish Inverted Hammer** candlestick pattern.
    /// This pattern is an Inverted Hammer that is colored bullish (green/white). 
    /// It is a single candle bullish reversal signal typically found at the bottom of a downtrend.
    /// </summary>
    public class Recognizer_InvertedHammerBullish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bullish Inverted Hammer Recognizer.
        /// </summary>
        public Recognizer_InvertedHammerBullish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bullish Inverted Hammer";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Bullish Inverted Hammer pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Bullish Inverted Hammer, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsBullishInvertedHammer' is true.
            return index >= 0 && index < list.Count && list[index].IsBullishInvertedHammer;
        }
    }
}