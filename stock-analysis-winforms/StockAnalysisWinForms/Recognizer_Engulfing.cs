using System;
using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Recognizer class for the **General Engulfing** two-candlestick pattern.
    /// This pattern occurs when the second candle's body completely covers the first candle's body, 
    /// regardless of color. Specific bullish or bearish rules are implemented in subclasses.
    /// </summary>
    public class Recognizer_Engulfing : Recognizer
    {
        /// <summary>
        /// Constructor for the Engulfing Recognizer.
        /// </summary>
        public Recognizer_Engulfing()
        {
            // Set the name of the pattern for display in the UI.
            Name = "Engulfing";
        }

        /// <summary>
        /// Overrides the base method to check if the two candlesticks ending at the given index 
        /// form a general Engulfing pattern.
        /// </summary>
        /// <param name="list">The list of SmartCandlestick objects (the price history).</param>
        /// <param name="index">The index of the second (current) candlestick in the pattern.</param>
        /// <returns>True if the pattern is recognized, False otherwise.</returns>
        public override bool Recognize(List<SmartCandlestick> list, int index)
        {
            // The Engulfing pattern requires at least two candlesticks (index 0 and index 1).
            // Check if the current index is valid and ensures there is a previous candle (index > 0).
            if (index <= 0 || index >= list.Count)
                return false;

            // Alias for the previous (first) candlestick.
            var prev = list[index - 1];
            // Alias for the current (second) candlestick.
            var curr = list[index];

            // Get the Open and Close values for the previous candle.
            decimal prevOpen = prev.Open;
            decimal prevClose = prev.Close;
            // Get the Open and Close values for the current candle.
            decimal currOpen = curr.Open;
            decimal currClose = curr.Close;

            // Determine the body extremes (min/max of Open/Close) for the previous candle.
            decimal bodyMinPrev = Math.Min(prevOpen, prevClose);
            decimal bodyMaxPrev = Math.Max(prevOpen, prevClose);
            // Determine the body extremes (min/max of Open/Close) for the current candle.
            decimal bodyMinCurr = Math.Min(currOpen, currClose);
            decimal bodyMaxCurr = Math.Max(currOpen, currClose);

            // Rule: The body of the current (second) candle must be strictly lower than the previous body's bottom 
            // AND strictly higher than the previous body's top to completely engulf it.
            // The condition uses strict inequality (< and >) to ensure engulfing.
            return (bodyMinCurr < bodyMinPrev && bodyMaxCurr > bodyMaxPrev);
        }
    }
}