using Stocks.Models;
using Stocks.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Stocks.Views
{
    public partial class SearchTicker : Window
    {
        SearchTickerVM vm;
        public SearchTicker()
        {
            InitializeComponent();
            vm = new SearchTickerVM();
            DataContext = vm;
        }

        void addTicker_click(object sender, RoutedEventArgs e)
        {
            TickerNamePair selectedPair = ((Button)sender).DataContext as TickerNamePair;
            vm.AddTickerToWatchList(selectedPair.Ticker);
        }
    }
}
