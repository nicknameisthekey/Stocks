using System.Collections.Generic;
using System.IO;

namespace Stocks
{
    public static class Settings
    {
        public static List<string> GetSavedWatchlist()
        {
            if (!File.Exists("Tickers.txt"))
            {
                File.Create("Tickers.txt");
                return new List<string>();
            }
            string allTickers = File.ReadAllText("Tickers.txt");
            string[] tickers = allTickers.Split(',');
            return new List<string>(tickers);
        }
        public static void SaveTickers(List<string> tickers)
        {
            File.WriteAllText("Tickers.txt", string.Join(",", tickers));
        }
        public static Dictionary<string, string> GetSavedInterfaxIds()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!File.Exists("InterfaxIds.txt"))
            {
                File.Create("InterfaxIds.txt");
                return result;
            }
            string[] allIds = File.ReadAllLines("InterfaxIds.txt");
            foreach (string pair in allIds)
            {
                string[] split = pair.Split(':');
                result.Add(split[0], split[1]);
            }
            return result;
        }
        public static void SaveInterfaxIds(Dictionary<string, string> ids)
        {
            string result = "";
            foreach (var pair in ids)
            {
                result += $"{pair.Key}:{pair.Value}\n";
            }
            File.WriteAllText("InterfaxIds.txt", result);
        }
    }
}
