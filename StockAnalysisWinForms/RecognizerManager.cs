using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Central controller that owns ALL recognizers
    /// and runs them on the current SmartCandlestick list.
    /// </summary>
    public class RecognizerManager
    {
        // List of all recognizers available in the UI (Doji, Hammer, Engulfing, etc.)
        public List<Recognizer> Recognizers { get; } = new List<Recognizer>();

        public RecognizerManager()
        {
            Recognizers.Add(new Recognizer_Doji());
            Recognizers.Add(new Recognizer_DragonflyDoji());
            Recognizers.Add(new Recognizer_GravestoneDoji());

            Recognizers.Add(new Recognizer_Marubozu());
            Recognizers.Add(new Recognizer_MarubozuBullish());
            Recognizers.Add(new Recognizer_MarubozuBearish());

            Recognizers.Add(new Recognizer_Hammer());
            Recognizers.Add(new Recognizer_HammerBullish());
            Recognizers.Add(new Recognizer_HammerBearish());

            Recognizers.Add(new Recognizer_InvertedHammer());
            Recognizers.Add(new Recognizer_InvertedHammerBullish());
            Recognizers.Add(new Recognizer_InvertedHammerBearish());

            Recognizers.Add(new Recognizer_Engulfing());
            Recognizers.Add(new Recognizer_EngulfingBullish());
            Recognizers.Add(new Recognizer_EngulfingBearish());

            Recognizers.Add(new Recognizer_Harami());
            Recognizers.Add(new Recognizer_HaramiBullish());
            Recognizers.Add(new Recognizer_HaramiBearish());
        }


        /// <summary>
        /// Clear all old results, then run each recognizer
        /// on the full SmartCandlestick list.
        /// </summary>
        public void RunAll(List<SmartCandlestick> smartList)
        {
            if (smartList == null || smartList.Count == 0)
                return;

            // First clear indices for EVERY recognizer
            foreach (var r in Recognizers)
                r.Indices.Clear();

            // Run each recognizer on the candle list
            foreach (var r in Recognizers)
                r.Run(smartList);
        }
    }
}
