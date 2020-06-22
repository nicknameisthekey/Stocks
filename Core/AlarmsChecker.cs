using Stocks.Models;
using System;

namespace Stocks.Core
{
    public static class AlarmsChecker
    {
        public static event Action<PriceAlarm> FirePriceAlarm;
        public static void Initialize()
        {
            PriceUpdater.WatchListPricesUpdated += onPricesUpdate;
        }
        static void onPricesUpdate()
        {
            TickerPrices[] watchlist = PriceUpdater.WatchlistPrices.ToArray();
            foreach (PriceAlarm alarm in SettingsManager.Settings.PriceAlarms.ToArray())
            {
                foreach (TickerPrices priceData in watchlist)
                {
                    if (priceData.Ticker != alarm.Ticker) continue;

                    bool condition1 = (alarm.AlarmIfTargetHigher &&
                                       priceData.CurrentPrice >= alarm.TargetPrice);
                    bool condition2 = (!alarm.AlarmIfTargetHigher
                                       && priceData.CurrentPrice < alarm.TargetPrice);

                    if (condition1 || condition2)
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
