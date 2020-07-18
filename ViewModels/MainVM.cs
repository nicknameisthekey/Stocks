using Stocks.Core;
using Stocks.Models;
using Stocks.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Stocks.ViewModels
{
    public class MainVM : BaseVM
    {
        List<CompanyData> watchList;
        string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                updateView();
                onPropertyChange(); //убрать вызов при изменении с формы
            }
        }
        public List<CompanyData> WatchList
        {
            get => watchList;
            set { watchList = value; onPropertyChange(); }
        }
        public MainVM()
        {
            WatchList = MOEXData.Watchlist;
            MOEXData.WatchListPricesUpdated += updateView;
        }
        public void updateView()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                WatchList = MOEXData.Watchlist;
            }
            else
            {
                WatchList = MOEXData.Watchlist
                    .Where(c => c.Ticker.Contains(SearchText.ToUpper())).ToList();
            }
        }
        public void RemoveTicker(string ticker)
        {
            SettingsManager.RemoveTicker(ticker);
        }
        public void ShowReportsWindow(CompanyData ticker)
        {
            if (SettingsManager.Settings.InterfaxIds.ContainsKey(ticker.Ticker))
            {
                ReportsWindow window = new ReportsWindow();
                window.DataContext = new ReportsVM
                    (SettingsManager.Settings.InterfaxIds[ticker.Ticker]);
                window.Show();
            }
            else
            {
                MessageBox.Show("Для этой компании не задан id на сайте интерфакс");
            }
        }
        public void ShowAlarmsWindow(CompanyData selected)
        {
            AlarmsWindow window = new AlarmsWindow();
            window.DataContext = new AlarmsVM(selected.Ticker);
            window.Show();
        }
        public void ShowFactsWindow(CompanyData ticker)
        {
            if (SettingsManager.Settings.InterfaxIds.ContainsKey(ticker.Ticker))
            {
                FactsWindow window = new FactsWindow();
                window.DataContext = new FactsVM
                    (SettingsManager.Settings.InterfaxIds[ticker.Ticker]);
                window.Show();
            }
            else
            {
                MessageBox.Show("Для этой компании не задан id на сайте интерфакс");
            }
        }
    }
}
