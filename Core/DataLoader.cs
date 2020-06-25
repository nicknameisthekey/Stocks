using HtmlAgilityPack;
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
        public static List<InterfaxData> LoadInterfax(string id, int year)
        {
            string uri = "http://e-disclosure.ru/Event/Page?companyId="
                + id + "&year=" + year;
            Stream str = WebRequest.Create(new Uri(uri)).GetResponse().GetResponseStream();
            string resp = new StreamReader(str).ReadToEnd();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(resp);

            int linksCount = htmlDoc.DocumentNode.SelectNodes("/table[1]/tr").Count;
            List<InterfaxData> result = new List<InterfaxData>();
            for (int i = 2; i <= linksCount; i++)
            {
                HtmlNode dateNode = htmlDoc.DocumentNode.SelectSingleNode($"/table[1]/tr[{i}]/td[1]");
                string date = dateNode.InnerText;
                HtmlNode x = htmlDoc.DocumentNode.SelectSingleNode($"/table[1]/tr[{i}]/td[3]/a");
                string link = x.Attributes["href"].Value;
                string text = x.InnerHtml;
                result.Add(new InterfaxData(link, text, date));
            }
            return result;
        }
        public static List<InterfaxData> LoadReportList(string interfaxID, int type)
        {
            List<InterfaxData> result = new List<InterfaxData>();
            string uri = "http://e-disclosure.ru/portal/files.aspx?id=" + interfaxID + "&type=" + type;
            Stream str = WebRequest.Create(new Uri(uri)).GetResponse().GetResponseStream();
            string resp = new StreamReader(str).ReadToEnd();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(resp);
            HtmlNodeCollection table = htmlDoc.DocumentNode.SelectNodes
                ("html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[5]/div[3]/table[1]/tr");
            foreach (var tr in table.Skip(1))
            {
                HtmlNode linkNode = tr.SelectSingleNode("td[7]/a");
                string link = linkNode.Attributes["data-fileinfo"].Value;
                HtmlNode reportNameNode = tr.SelectSingleNode("td[3]");
                string reportName = reportNameNode.InnerText;
                HtmlNode postDateNode = tr.SelectSingleNode("td[5]");
                string postdate = postDateNode.InnerText;
                result.Add(new InterfaxData(link, reportName, postdate));
            }
            return result;
        }
        //static void unzip()
        //{
        //    ZipFile.ExtractToDirectory("temp.zip", "temp");
        //    Process.Start(new ProcessStartInfo(files[0]));
        //}
    }
}
