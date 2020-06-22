using Stocks.Models;
using Stocks.Views;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class AlarmsVM : BaseVM
    {
        public List<PriceAlarm> PriceAlarms
        {
            get => priceAlarms;
            private set { priceAlarms = value; onPropertyChange(); }
        }
        TickerPrices selectedTicker;
        List<PriceAlarm> priceAlarms;

        public AlarmsVM()
        {
            MainVM.SelectedItemChanged += OnSelectedCompanychange;
            SettingsManager.SettingsChanged += updateAlarmList;
        }
        void updateAlarmList()
        {
            PriceAlarms = SettingsManager.Settings.PriceAlarms
                .Where(a => a.Ticker == selectedTicker.Ticker).ToList();
        }
        private void OnSelectedCompanychange(TickerPrices ticker)
        {
            selectedTicker = ticker;
            updateAlarmList();
        }
        public void onAddNewAlarm()
        {
            AddAlarm window = new AddAlarm(selectedTicker.Ticker);
            window.Show();
        }
        public void onRemoveAlarm(PriceAlarm alarm)
        {
            SettingsManager.RemovePriceAlarm(alarm);
        }
    }
}
