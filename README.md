# Stock Analysis WinForms

A Windows desktop app (C# / .NET Framework 4.7.2 / WinForms) for charting stock price history and detecting candlestick patterns.



## Features

- Load one or more OHLCV CSV files (daily, weekly, or monthly) and open a chart window for each
- Candlestick price chart with a volume chart underneath, for a selectable date range
- Detects candlestick patterns across the loaded history, including:
  - **Doji**, Dragonfly Doji, Gravestone Doji
  - **Marubozu** (bullish and bearish)
  - **Hammer** and **Inverted Hammer** (bullish and bearish)
  - **Engulfing** (bullish and bearish)
  - **Harami** (bullish and bearish)
- Sample data for 42 tickers (AAPL, AMZN, TSLA, JPM, and others) in `Stock Data/`

## Tech Stack

- C# on .NET Framework 4.7.2
- Windows Forms with the built-in `DataVisualization.Charting` control
- Pattern detection built on a common `Recognizer` base class, with a `RecognizerManager` that runs every pattern

## Project Structure

```
StockAnalysisWinForms.sln          Visual Studio solution
StockAnalysisWinForms/             WinForms project
  Form_Home.cs                     Launcher: pick tickers and a date range
  Form_Display.cs                  Candlestick and volume chart for one ticker
  StockReader.cs                   Parses CSV files into candlestick data
  Candlestick.cs                   Price data model
  SmartCandlestick.cs              Candle with derived properties for pattern checks
  Recognizer*.cs                   One class per candlestick pattern
  RecognizerManager.cs            Runs all recognizers
Stock Data/                        Sample CSV data (Ticker, Period, Date, OHLCV, LogReturn, Sector)
```

## Getting Started

### Prerequisites

- Windows 10 or 11
- Visual Studio 2022 with the **.NET desktop development** workload (includes .NET Framework 4.7.2 targeting)

### Run it

1. Clone the repository and open `StockAnalysisWinForms.sln` in Visual Studio.
2. Press **F5** (or Debug → Start Debugging).
3. In the launcher window, set a date range, click **Load**, and pick one or more CSV files from `Stock Data/`.

## Design Notes

- Each candlestick pattern is its own class so new patterns can be added without changing the chart code.
- `StockReader` maps CSV columns by header name, so column order doesn't matter.
- `RecognizerManager` clears old results before each run, so changing the date range never shows stale patterns.

## Roadmap

- [ ] Export detected patterns to CSV
- [ ] Support for loading data directly from a market data API

## License

MIT. See [LICENSE](LICENSE).
