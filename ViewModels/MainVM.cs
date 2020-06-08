using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class MainVM : BaseVM
    {
        List<Company> companiesToShow;
        string searchText;
        public string SearchText
        {
            get => searchText;
            set 
            { 
                searchText = value;
                updateList();
                onPropertyChange(); //убрать вызов при изменении с формы
            }
        }
        public List<Company> CompaniesToShow
        {
            get => companiesToShow;
            set { companiesToShow = value; onPropertyChange(); }
        }
        public MainVM()
        {
            CompaniesToShow = DataLoader.Companies;
            DataLoader.PricesUpdated += updatePrices;
        }
        public void AddTicker(string ticker) =>
            Settings.AddTicker(ticker.Trim().ToUpper());
        public void RemoveTicker(string ticker)
        => Settings.RemoveTicker(ticker);
        public void updateList()
        {
            if (string.IsNullOrEmpty(SearchText)) CompaniesToShow = DataLoader.Companies;
            else
            {
                CompaniesToShow = DataLoader.Companies
                    .Where(c => c.Ticker.Contains(SearchText.ToUpper())).ToList();
            }
        }
        void updatePrices() { updateList(); }

    }
}
