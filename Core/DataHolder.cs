using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Stocks.Core
{
    public static class DataHolder
    {
        public static Action WatchListPricesUpdated;
        public static List<TickerPrices> WatchlistPrices { get; private set; }
        public static List<TickerNamePair> ListedTickers { get; private set; }

        static List<string> watchlistTickers;
        static Timer timer;
        public static void Initialize()
        {
            watchlistTickers = Settings.GetSavedWatchlist();
            ListedTickers = DataLoader.LoadListedTickers();
            startTimer();
        }
        public static void AddTickerToWatch(string ticker)
        {
            watchlistTickers.Add(ticker);
            Settings.SaveTickers(watchlistTickers);
            getActualPrices();
        }
        public static void RemoveTickerToWatch(string ticker)
        {
            watchlistTickers.Remove(ticker);
            Settings.SaveTickers(watchlistTickers);
            getActualPrices();
        }
        static void getActualPrices()
        {
            WatchlistPrices = new List<TickerPrices>();
            foreach (string ticker in watchlistTickers)
            {
                WatchlistPrices.Add(DataLoader.LoadPrice(ticker));
            }
            WatchListPricesUpdated?.Invoke();
        }

        #region timer
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
            getActualPrices();
            startTimer();
        }
        #endregion
    }
}
