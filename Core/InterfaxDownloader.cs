using HtmlAgilityPack;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Stocks
{
    static class DataDownloader
    {
        /// <summary>
        /// Загрузка существенных фактов с интерфакса
        /// </summary>
        public static List<InterfaxData> LoadCompanyEvents(string id, int year)
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

        /// <summary>
        /// Загрузка списка отчётов, доступных для загрузка
        /// </summary>
        public static List<InterfaxData> LoadReportsList(string interfaxID, int type)
        {
            List<InterfaxData> result = new List<InterfaxData>();
            try
            {
                string uri = "http://e-disclosure.ru/portal/files.aspx?id=" + interfaxID + "&type=" + type;
                Stream str = WebRequest.Create(new Uri(uri)).GetResponse().GetResponseStream();
                string resp = new StreamReader(str).ReadToEnd();
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(resp);
                HtmlNodeCollection table = htmlDoc.DocumentNode.SelectNodes
                    ("html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[5]/div[3]/table[1]/tr");
                foreach (var tr in table.Skip(1))
                {
                    HtmlNode linkNode = tr.SelectSingleNode("td[6]/a");
                    if (linkNode == null)
                        continue;
                    string link = linkNode.Attributes["data-fileinfo"].Value;
                    HtmlNode reportNameNode = tr.SelectSingleNode("td[2]");
                    string reportName = reportNameNode.InnerText;
                    HtmlNode postDateNode = tr.SelectSingleNode("td[5]");
                    string postdate = postDateNode.InnerText;
                    result.Add(new InterfaxData(link, reportName, postdate));
                }
            }
            catch (Exception e) { }
            return result;
        }
    }
}
