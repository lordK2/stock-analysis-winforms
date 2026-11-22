using System;

namespace StockAnalysisWinForms
{
    /// <summary>
    /// Smarter version of Candlestick: exposes anatomy and pattern boolean flags.
    /// Used by recognizers (Doji, Hammer, Engulfing, etc).
    /// </summary>
    public class SmartCandlestick : Candlestick
    {
        public SmartCandlestick() { }

        /// <summary>
        /// Wrap an existing Candlestick (copy values).
        /// </summary>
        public SmartCandlestick(Candlestick c)
        {
            Date = c.Date;
            Open = c.Open;
            High = c.High;
            Low = c.Low;
            Close = c.Close;
            Volume = c.Volume;
        }

        // ----------- Basic Anatomy -----------

        /// <summary>Total candle height (High - Low).</summary>
        public decimal Range => High - Low;

        /// <summary>Absolute body size (|Open - Close|).</summary>
        public decimal BodyRange => Math.Abs(Open - Close);

        /// <summary>Upper wick length.</summary>
        public decimal UpperTailRange
        {
            get
            {
                decimal bodyTop = Math.Max(Open, Close);
                return High - bodyTop;
            }
        }

        /// <summary>Lower wick length.</summary>
        public decimal LowerTailRange
        {
            get
            {
                decimal bodyBottom = Math.Min(Open, Close);
                return bodyBottom - Low;
            }
        }

        // ----------- Bullish / Bearish -----------

        public bool IsBullish => Close > Open;
        public bool IsBearish => Close < Open;

        // ----------- 1-Candle Patterns -----------

        public bool IsDoji
        {
            get
            {
                if (Range <= 0) return false;
                return BodyRange <= Range * 0.10m;  // body < 10%
            }
        }

        public bool IsDragonflyDoji
        {
            get
            {
                if (!IsDoji) return false;
                return UpperTailRange <= Range * 0.05m && LowerTailRange >= Range * 0.30m;
            }
        }

        public bool IsGravestoneDoji
        {
            get
            {
                if (!IsDoji) return false;
                return LowerTailRange <= Range * 0.05m && UpperTailRange >= Range * 0.30m;
            }
        }

        public bool IsMarubozu
        {
            get
            {
                if (Range <= 0) return false;
                return UpperTailRange < Range * 0.05m && LowerTailRange < Range * 0.05m;
            }
        }

        public bool IsBullishMarubozu => IsMarubozu && IsBullish;
        public bool IsBearishMarubozu => IsMarubozu && IsBearish;

        public bool IsHammer
        {
            get
            {
                if (BodyRange <= 0) return false;
                return LowerTailRange >= BodyRange * 2m && UpperTailRange <= BodyRange * 0.30m;
            }
        }

        public bool IsBullishHammer => IsHammer && IsBullish;
        public bool IsBearishHammer => IsHammer && IsBearish;

        public bool IsInvertedHammer
        {
            get
            {
                if (BodyRange <= 0) return false;
                return UpperTailRange >= BodyRange * 2m && LowerTailRange <= BodyRange * 0.30m;
            }
        }

        public bool IsBullishInvertedHammer => IsInvertedHammer && IsBullish;
        public bool IsBearishInvertedHammer => IsInvertedHammer && IsBearish;

        // Add more patterns as needed for 2-candle recognizers (Engulfing, Harami handled externally)
    }
}
