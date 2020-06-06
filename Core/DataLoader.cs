using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Stocks
{
    static class DataLoader
    {
        public static event Action<List<Price>> PricesUpdated;
        public static event Action TickersLoaded;
        public static readonly List<TempPair> TickersListed
            = new List<TempPair>();
        static Timer timer;
        static DataLoader()
        {
            Task.Run(() => LoadListedTickers());
            startTimer();
        }
        public static void LoadListedTickers()
        {
            string s = "https://iss.moex.com/iss/engines/stock/markets/shares/securities.csv";
            WebResponse resp = WebRequest.Create(new Uri(s)).GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251));
            string[] asnwer = sr.ReadToEnd().Split(Environment.NewLine.ToCharArray()).Skip(3).ToArray();
            IEnumerable<string> answerFiltered = asnwer.Where(a => a.Contains("TQBR") && a.Contains("Акции и ДР"));
            foreach (string a in answerFiltered)
                TickersListed.Add(new TempPair(a.Split(';')[0], a.Split(';')[9]));
            TickersLoaded?.Invoke();
        }
        public static Price LoadPrice(string ticker)
        {
            ticker = ticker.Trim().ToUpper();
            string s = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.csv?securities=" +
                ticker + "&iss.meta=on&iss.only=marketdata";
            System.Threading.Thread.CurrentThread.CurrentCulture
                = CultureInfo.InvariantCulture;

            WebResponse resp = WebRequest.Create(new Uri(s)).GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string answer = sr.ReadToEnd();
            string[] asnwersplit = answer.Split(Environment.NewLine.ToCharArray()).Skip(2).ToArray();
            string[] headers = asnwersplit[0].Split(';');
            string[] data = asnwersplit[1].Split(';');
            string open = data[Array.IndexOf(headers, "OPEN")];
            string low = data[Array.IndexOf(headers, "LOW")];
            string high = data[Array.IndexOf(headers, "HIGH")];
            string last = data[Array.IndexOf(headers, "LAST")];
            return new Price(ticker, open, low, high, last);
        }
        static void startTimer()
        {
            timer = new Timer(1500);
            timer.Elapsed += onTimer;
            timer.Start();
        }
        static void stopTimer()
        {
            timer.Stop();
            timer.Elapsed -= onTimer;
            timer = null;
        }
        static void onTimer(object s, ElapsedEventArgs e)
        {
            stopTimer();
            List<Price> prices = loadPrices();
            PricesUpdated?.Invoke(prices);
            startTimer();
        }
        static List<Price> loadPrices()
        {
            List<Price> prices = new List<Price>();
            foreach (string ticker in Settings.Tickers)
                prices.Add(LoadPrice(ticker));
            return prices;
        }
    }
}
