using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace Stocks
{
    class DataLoader
    {
        public static Price LoadData(string ticker)
        {
            ticker = ticker.Trim().ToUpper();
            string s = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.csv?securities=" +
                ticker + "&iss.meta=on&iss.only=marketdata";
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

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

    }
}
