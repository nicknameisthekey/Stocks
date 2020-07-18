using Stocks.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class AlarmsVM : BaseVM
    {
        readonly string ticker;
        public List<PriceAlarm> PriceAlarms
        {
            get => priceAlarms;
            private set { priceAlarms = value; onPropertyChange(); }
        }
        List<PriceAlarm> priceAlarms;
        public AlarmsVM(string ticker)
        {
            this.ticker = ticker;
            SettingsManager.SettingsChanged += updateAlarmList;
        }
        void updateAlarmList()
        {
            PriceAlarms = SettingsManager.Settings.PriceAlarms
                .Where(a => a.Ticker == ticker).ToList();
        }
        public void AddNewAlarm(string price, bool higher)
        {
            PriceAlarm alarm = new PriceAlarm(ticker, float.Parse(price), higher);
            SettingsManager.AddPriceAlarm(alarm);
        }
        public void onRemoveAlarm(PriceAlarm alarm)
        {
            SettingsManager.RemovePriceAlarm(alarm);
        }
    }
}
