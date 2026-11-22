using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Bullish Marubozu** candlestick pattern.
    /// This pattern is a strong single-candle indicator of overwhelming buying pressure,
    /// characterized by a long bullish body with little to no upper or lower wick. 
    /// The Close price is the High, and the Open price is the Low (or very close to them).
    /// </summary>
    public class Recognizer_MarubozuBullish : Recognizer
    {
        /// <summary>
        /// Constructor for the Bullish Marubozu Recognizer.
        /// </summary>
        public Recognizer_MarubozuBullish()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Bullish Marubozu";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Bullish Marubozu pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Bullish Marubozu, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsBullishMarubozu' is true.
            return index >= 0 && index < list.Count && list[index].IsBullishMarubozu;
        }
    }
}