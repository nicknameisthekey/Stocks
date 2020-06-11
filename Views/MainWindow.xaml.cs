using Stocks.Core;
using Stocks.Models;
using Stocks.ViewModels;
using Stocks.Views;
using System;
using System.Windows;

namespace Stocks
{
    public partial class MainWindow : Window
    {
        MainVM vm;
        public MainWindow()
        {
            DataHolder.Initialize();
            InitializeComponent();
            vm = new MainVM();
            DataContext = vm;
        }
        private void addNewTicker_Click(object sender, RoutedEventArgs e)
        {
            SearchTicker searchTicker = new SearchTicker();
            searchTicker.Show();
        }

        private void deleteTicker_click(object sender, RoutedEventArgs e)
        {
            TickerPrices selected = (TickerPrices)Watchlist.SelectedItem;
            vm.RemoveTicker(selected.Ticker);
        }
    }
}
