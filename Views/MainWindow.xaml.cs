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
            prices.Add(DataLoader.LoadData("SBER"));
            prices.Add(DataLoader.LoadData("SBERP"));
            prices.Add(DataLoader.LoadData("DSKY"));
            prices.Add(DataLoader.LoadData("ALRS"));
            DataContext = new MainVM(prices);
        }
    }
}
