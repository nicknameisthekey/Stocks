using Stocks.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class FactsVM : BaseVM
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
        public FactsVM(string id)
        {
            selectedTickerData = DataDownloader.LoadCompanyEvents(id, 2020);
            updateList();
        }
        void updateList()
        {
            FactsList = selectedTickerData.Where
                (d => d.Text.ToLower().Contains(searchString.ToLower())).ToList();
        }
    }
}
