using System.Collections.Generic;

namespace Stocks.ViewModels
{
    public class MainVM : BaseVM
    {
        List<Price> prices;
        bool changeButtonsEnabled;
        List<TempPair> listedTickers;

        public bool ChangeButtonsEnabled
        {
            get => changeButtonsEnabled;
            private set { changeButtonsEnabled = value; onPropertyChange(); }
        }
        public List<Price> Prices
        {
            get => prices;
            set { prices = value; onPropertyChange(); }
        }
        public List<TempPair> ListedTickers
        {
            get => listedTickers;
            set { listedTickers = value; onPropertyChange(); }
        }
        public MainVM()
        {
            DataLoader.PricesUpdated += updatePrices;
            DataLoader.TickersLoaded += () => ListedTickers = DataLoader.TickersListed;
        }
        public void EditTickerListBtn_Click() =>
            ChangeButtonsEnabled = !ChangeButtonsEnabled;
        public void AddTicker(string ticker) =>
            Settings.AddTicker(ticker.Trim().ToUpper());
        public void RemoveTicker(string ticker)
        => Settings.RemoveTicker(ticker);
        void updatePrices(List<Price> prices) => Prices = prices;
    }
}
