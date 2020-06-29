using Stocks.Models;
using System;
using System.Collections.Generic;
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
        public static List<TickerPrices> WatchlistPrices { get; private set; }
        /// <summary>
        /// Список всех доступных тикеров
        /// </summary>
        public static List<TickerNamePair> ListedTickers { get; private set; }
        /// <summary>
        /// Запуск работы
        /// </summary>
        public static void Initialize()
        {
            SettingsManager.SettingsChanged += updateWatchListPrices;
            ListedTickers = DataDownloader.LoadListedTickers();
            updateWatchListPrices();
            startTimer();
        }
        static void updateWatchListPrices()
        {
            WatchlistPrices = new List<TickerPrices>();
            foreach (string ticker in SettingsManager.Settings.WatchListTickers)
            {
                WatchlistPrices.Add(DataDownloader.LoadPrice(ticker));
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
