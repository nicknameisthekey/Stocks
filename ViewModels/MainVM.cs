using Stocks.Core;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class MainVM : BaseVM
    {
        public static Action<TickerPrices> SelectedItemChanged;
        List<TickerPrices> watchListPrices;
        string searchText;
        private TickerPrices selectedItem;

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
        public TickerPrices SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                SelectedItemChanged?.Invoke(value);
                onPropertyChange();
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
