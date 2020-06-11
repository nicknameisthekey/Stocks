using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Stocks
{
    static class DataLoader
    {
        public static List<TickerNamePair> LoadListedTickers()
        {
            List<TickerNamePair> result = new List<TickerNamePair>();
            string s = "https://iss.moex.com/iss/engines/stock/markets/shares/securities.csv";
            WebResponse resp = WebRequest.Create(new Uri(s)).GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251));
            string[] asnwer = sr.ReadToEnd().Split(Environment.NewLine.ToCharArray()).Skip(3).ToArray();
            IEnumerable<string> answerFiltered = asnwer.Where(a => a.Contains("TQBR") && a.Contains("Акции и ДР"));
            foreach (string a in answerFiltered)
                result.Add(new TickerNamePair(a.Split(';')[0], a.Split(';')[9]));
            return result;
        }
        public static TickerPrices LoadPrice(string ticker)
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
            return new TickerPrices(ticker, open, low, high, last);
        }
    }
}
