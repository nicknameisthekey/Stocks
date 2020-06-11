using Stocks.Core;
using Stocks.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class MainVM : BaseVM
    {
        List<TickerPrices> watchListPrices;
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
        public List<TickerPrices> PricesToShow
        {
            get => watchListPrices;
            set { watchListPrices = value; onPropertyChange(); }
        }
        public MainVM()
        {
            PricesToShow = DataHolder.WatchlistPrices;
            DataHolder.WatchListPricesUpdated += updateList;
        }
        public void updateList()
        {
            if (string.IsNullOrEmpty(SearchText)) PricesToShow = DataHolder.WatchlistPrices;
            else
            {
                PricesToShow = DataHolder.WatchlistPrices
                    .Where(c => c.Ticker.Contains(SearchText.ToUpper())).ToList();
            }
        }
        public void RemoveTicker(string ticker)
        {
            DataHolder.RemoveTickerToWatch(ticker);
        }
    }
}
