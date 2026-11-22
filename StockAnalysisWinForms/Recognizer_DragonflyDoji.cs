using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **Dragonfly Doji** candlestick pattern.
    /// This pattern is characterized by the open, close, and high prices being virtually the same, 
    /// with a long lower shadow (wick). It often signals a potential bullish reversal.
    /// </summary>
    public class Recognizer_DragonflyDoji : Recognizer
    {
        /// <summary>
        /// Constructor for the Dragonfly Doji Recognizer.
        /// </summary>
        public Recognizer_DragonflyDoji()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Dragonfly Doji";
        }

        /// <summary>
        /// Overrides the base method to check if a candlestick at a specific index 
        /// qualifies as a Dragonfly Doji pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the candlestick to examine.</param>
        /// <returns>True if the candlestick is a Dragonfly Doji, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // Check for a valid index within the list bounds, 
            // and then check if the SmartCandlestick's built-in property 'IsDragonflyDoji' is true.
            return index >= 0 && index < list.Count && list[index].IsDragonflyDoji;
        }
    }
}