using System.Collections.Generic;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Base recognizer class. Each concrete recognizer only checks ONE candle
    /// by overriding Recognize(list, index). This base class handles the loop.
    /// </summary>
    public abstract class Recognizer
    {
        public string Name { get; protected set; }

        // Store all matching indices for this pattern
        public List<int> Indices { get; } = new List<int>();

        /// <summary>
        /// Remove previous match results.
        /// </summary>
        public void Reset()
        {
            Indices.Clear();
        }

        /// <summary>
        /// Each derived recognizer checks ONE candle at a given index.
        /// Return true if the pattern is present at that index.
        /// </summary>
        public abstract bool Recognize(List<SmartCandlestick> list, int index);

        /// <summary>
        /// Run recognizer over entire candle list.
        /// </summary>
        public void Run(List<SmartCandlestick> list)
        {
            Reset();  // clear old results

            for (int i = 0; i < list.Count; i++)
            {
                if (Recognize(list, i))
                    Indices.Add(i);
            }
        }
    }
}
