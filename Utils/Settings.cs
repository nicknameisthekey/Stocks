using System.Collections.Generic;
using System.IO;

namespace Stocks
{
    public static class Settings
    {
        public static List<string> GetSavedWatchlist()
        {
            string allTickers = File.ReadAllText("Tickers.txt");
            string[] tickers = allTickers.Split(',');
            return new List<string>(tickers);
        }
        public static void SaveTickers(List<string> tickers)
        {
            File.WriteAllText("Tickers.txt", string.Join(",", tickers));
        }
    }
}
