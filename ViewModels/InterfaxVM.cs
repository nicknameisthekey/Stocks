using Stocks.Models;
using System.Collections.Generic;

namespace Stocks.ViewModels
{
    public class InterfaxVM : BaseVM
    {
        List<InterfaxData> factsList;
        public List<InterfaxData> FactsList
        {
            get => factsList;
            private set { factsList = value; onPropertyChange(); }
        }
        public InterfaxVM()
        {
            MainVM.SelectedItemChanged += OnSelectedCompanychange;
        }
        public void OnSelectedCompanychange(TickerPrices ticker)
        {
            var ids = Settings.GetSavedInterfaxIds();
            if (ids.ContainsKey(ticker.Ticker))
            {
                FactsList = DataLoader.LoadInterfax(ids[ticker.Ticker], 2020);
            }
        }

    }
}
