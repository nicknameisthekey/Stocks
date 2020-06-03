using Stocks.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace Stocks
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Price> prices = new List<Price>();
            foreach (string ticker in Settings.Tickers)
                prices.Add(DataLoader.LoadData(ticker));
            DataContext = new MainVM(prices);
        }
    }
}
