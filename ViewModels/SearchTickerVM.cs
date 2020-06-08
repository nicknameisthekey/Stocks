using Stocks.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.ViewModels
{
    public class SearchTickerVM : BaseVM
    {
        private List<TickerNamePair> notAddedTickers;
        private string searchText ="";

        public List<TickerNamePair> NotAddedTickers
        {
            get => notAddedTickers;
            set { notAddedTickers = value; onPropertyChange(); }
        }
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                updateList();
                onPropertyChange();
            }
        }
        public SearchTickerVM()
        {
            updateList();
        }

        private void updateList()
        {
            IEnumerable<string> tickersAdded
                            = DataLoader.Companies.Select(c => c.Ticker);
            NotAddedTickers = DataLoader.TickersListed
            .Where(t => !tickersAdded.Contains(t.Ticker) 
            && t.Ticker.Contains(SearchText.ToUpper())).ToList();
        }
    }
}
