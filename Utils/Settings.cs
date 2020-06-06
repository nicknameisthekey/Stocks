using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Stocks
{
    public static class Settings
    {
        public static readonly List<string> Tickers;
        static Settings()
        {
            string allTickers = File.ReadAllText("Tickers.txt");
            string[] tickers = allTickers.Split(',');
            Tickers = new List<string>(tickers);
        }
        public static void AddTicker(string ticker)
        {
            if (Tickers.Contains(ticker)) return; //todo
            Tickers.Add(ticker);

            File.AppendAllText("Tickers.txt", "," + ticker);
        }
        public static void RemoveTicker(string ticker)
        {
            Tickers.Remove(ticker);
            File.WriteAllText("Tickers.txt", string.Join(",", Tickers));
        }
    }
}
