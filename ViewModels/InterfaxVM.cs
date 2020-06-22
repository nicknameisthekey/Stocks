using Stocks.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class InterfaxVM : BaseVM
    {
        List<InterfaxData> factsList;
        string searchString = "";
        List<InterfaxData> selectedTickerData;
        public List<InterfaxData> FactsList
        {
            get => factsList;
            private set { factsList = value; onPropertyChange(); }
        }
        public string SearchString
        {
            get => searchString;
            set { searchString = value; onPropertyChange(); updateList(); }
        }
        public InterfaxVM()
        {
            MainVM.SelectedItemChanged += OnSelectedCompanychange;
        }
        public void OnSelectedCompanychange(TickerPrices ticker)
        {
            var ids = SettingsManager.Settings.InterfaxIds;
            if (ids.ContainsKey(ticker.Ticker))
            {
                selectedTickerData = DataLoader.LoadInterfax(ids[ticker.Ticker], 2020);
            }
            else
                selectedTickerData = new List<InterfaxData>();
            updateList();

        }
        void updateList()
        {
            FactsList = selectedTickerData.Where
                (d => d.Text.ToLower().Contains(searchString.ToLower())).ToList();
        }

    }
}
