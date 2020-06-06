using Stocks.ViewModels;
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

        private void changeTickerList_Click(object sender, RoutedEventArgs e)
        {
            vm.EditTickerListBtn_Click();
            if (vm.ChangeButtonsEnabled)
                changeTickerListBtn.Content = "Сохранить";
            else
                changeTickerListBtn.Content = "Изменить";
        }

        private void addTicker_Click(object sender, RoutedEventArgs e)
        {
            vm.AddTicker(NewTickerTB.Text);
        }

        private void removeTicker_Click(object sender, RoutedEventArgs e)
        {
            Price p = (Price)((Button)sender).DataContext;
            vm.RemoveTicker(p.Ticker);
        }
    }
}
