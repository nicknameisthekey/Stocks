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
            var sr = Application.GetContentStream(new Uri("Tickers.txt", UriKind.Relative));
            string allTickers = new StreamReader(sr.Stream).ReadToEnd();
            string[] tickers = allTickers.Split(',');
            Tickers = new List<string>(tickers);
        }
    }
}
