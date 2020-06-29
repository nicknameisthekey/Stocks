using Stocks.Models;
using System;
using System.Collections.Generic;

namespace Stocks.Core
{
    /// <summary>
    /// Класс, отвечающий за проверку срабатывания уведомлений о ценах
    /// </summary>
    public static class AlarmsChecker
    {
        /// <summary>
        /// Вызывается когда цена удовлетворяет условию уведомления
        /// </summary>
        public static event Action<PriceAlarm> FirePriceAlarm;
        /// <summary>
        /// Инициализация
        /// </summary>
        public static void Initialize()
        {
            MOEXData.WatchListPricesUpdated += onPricesUpdate;
        }
        /// <summary>
        /// Сравнение цен и условий уведомления
        /// </summary>
        static void onPricesUpdate()
        {
            List<TickerPrices> watchlist = MOEXData.WatchlistPrices;
            foreach (PriceAlarm alarm in SettingsManager.Settings.PriceAlarms.ToArray())
            {
                foreach (TickerPrices priceData in watchlist)
                {
                    if (priceData.Ticker != alarm.Ticker) continue;

                    if (alarm.AlarmIfTargetHigher
                                       && priceData.CurrentPrice >= alarm.TargetPrice
                        || !alarm.AlarmIfTargetHigher
                                       && priceData.CurrentPrice < alarm.TargetPrice)
                    {
                        FirePriceAlarm?.Invoke(alarm);
                        SettingsManager.RemovePriceAlarm(alarm);
                    }
                    break;
                }
            }
        }
    }
}
