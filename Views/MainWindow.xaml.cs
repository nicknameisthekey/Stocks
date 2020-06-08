using Stocks.ViewModels;
using Stocks.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Stocks
{
    public partial class MainWindow : Window
    {
        MainVM vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainVM();
            DataContext = vm;
        }
        private void removeTicker_Click(object sender, RoutedEventArgs e)
        {
            Company p = (Company)((Button)sender).DataContext;
            vm.RemoveTicker(p.Ticker);
        }
        private void addNewTicker_Click(object sender, RoutedEventArgs e)
        {
            SearchTicker searchTicker = new SearchTicker();
            searchTicker.DataContext = new SearchTickerVM();
            searchTicker.Show();
        }
    }
}
