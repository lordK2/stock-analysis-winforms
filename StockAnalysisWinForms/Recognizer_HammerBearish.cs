using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bearish Hammer** candlestick pattern.
    /// In the context of a downtrend, this pattern is equivalent to the Hanging Man pattern 
    /// but is colored bearish (red/black). It typically signals a potential bullish reversal 
    /// if confirmed by the following candle.
    /// </summary>
    public class Recognizer_HammerBearish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bearish Hammer Recognizer.
        /// </summary>
        public Recognizer_HammerBearish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bearish Hammer";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Bearish Hammer pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Bearish Hammer, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsBearishHammer' is true.
            return index >= 0 && index < list.Count && list[index].IsBearishHammer;
        }
    }
}