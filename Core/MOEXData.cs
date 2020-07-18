using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;

namespace Stocks.Core
{
    /// <summary>
    /// Хранит и обновляет биржевые данные
    /// </summary>
    public static class MOEXData
    {
        /// <summary>
        /// Таймер-триггер обновления цен
        /// </summary>
        static Timer timer;
        /// <summary>
        /// Вызывается после успешной загрузки данных с биржи
        /// </summary>
        public static Action WatchListPricesUpdated;
        /// <summary>
        /// Последние полученные данные о ценах с биржи
        /// </summary>
        public static List<CompanyData> Watchlist { get; private set; }
        /// <summary>
        /// Список всех доступных тикеров
        /// </summary>
        public static List<TickerNamePair> ListedTickers { get; private set; }
        /// <summary>
        /// Запуск работы
        /// </summary>
        public static void Initialize()
        {
            ListedTickers = LoadListedTickers();
            SettingsManager.SettingsChanged += updateWatchListPrices;
            updateWatchListPrices();
            startTimer();
        }
        static void updateWatchListPrices()
        {
            Watchlist = new List<CompanyData>();
            foreach (string ticker in SettingsManager.Settings.WatchListTickers)
            {
                TickerPrices prices = LoadPrice(ticker);
                TickerNamePair names = ListedTickers.First(n => n.Ticker == ticker);

                Watchlist.Add(new CompanyData(names.Name, names.Name, ticker, prices));
            }
            WatchListPricesUpdated?.Invoke();
        }
        /// <summary>
        /// Выгрузка всех эмитентов акций 
        /// </summary>
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
        /// <summary>
        /// Загрузка текущих цен для указанного тикера
        /// </summary>
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
            return new TickerPrices(open, low, high, last);
        }
        #region timer
        static void startTimer()
        {
            timer = new Timer(3000);
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
            updateWatchListPrices();
            startTimer();
        }
        #endregion
    }
}
