﻿using Stocks.Core;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.ViewModels
{
    public class SearchTickerVM : BaseVM
    {
        List<TickerNamePair> notAddedTickers;
        string searchText = "";
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
        public void AddTickerToWatchList(string ticker)
        {
            SettingsManager.AddTicker(ticker);
            updateList();
        }
        private void updateList()
        {
            IEnumerable<string> tickersAdded
                            = MOEXData.Watchlist.Select(c => c.Ticker);
            NotAddedTickers = MOEXData.ListedTickers
            .Where(t => !tickersAdded.Contains(t.Ticker)
            && t.Ticker.Contains(SearchText.ToUpper())).ToList();
        }
    }
}
