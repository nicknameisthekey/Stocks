using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Stocks.Core
{
    public static class PriceUpdater
    {
        public static Action WatchListPricesUpdated;
        public static List<TickerPrices> WatchlistPrices { get; private set; }
        public static List<TickerNamePair> ListedTickers { get; private set; }
        static Timer timer;
        public static void Initialize()
        {
            SettingsManager.SettingsChanged += updateWatchListPrices;
            ListedTickers = DataLoader.LoadListedTickers();
            updateWatchListPrices();
            startTimer();
        }
        static void updateWatchListPrices()
        {
            WatchlistPrices = new List<TickerPrices>();
            foreach (string ticker in SettingsManager.Settings.WatchListTickers)
            {
                WatchlistPrices.Add(DataLoader.LoadPrice(ticker));
            }
            WatchListPricesUpdated?.Invoke();
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
