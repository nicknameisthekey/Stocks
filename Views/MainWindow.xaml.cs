using Stocks.Core;
using Stocks.Models;
using Stocks.ViewModels;
using Stocks.Views;
using System.Windows;

namespace Stocks
{
    public partial class MainWindow : Window
    {
        MainVM vm;
        public MainWindow()
        {
            SettingsManager.ReadSettings();
            MOEXData.Initialize();
            AlarmsChecker.Initialize();
            AlarmsChecker.FirePriceAlarm += showPriceAlarm;

            InitializeComponent();
            vm = new MainVM();
            DataContext = vm;
        }
        void addNewTicker_Click(object sender, RoutedEventArgs e)
        {
            SearchTicker searchTicker = new SearchTicker();
            searchTicker.Show();
        }

        void deleteTicker_click(object sender, RoutedEventArgs e)
        {
            CompanyData selected = (CompanyData)Watchlist.SelectedItem;
            vm.RemoveTicker(selected.Ticker);
        }

        void changeInterfaxId_Click(object sender, RoutedEventArgs e)
        {
            CompanyData selected = (CompanyData)Watchlist.SelectedItem;
            ChangeInterfaxId window = new ChangeInterfaxId(selected.Ticker);
            window.Show();
        }
        void showPriceAlarm(PriceAlarm alarm)
        {
            if (alarm.AlarmIfTargetHigher)
                MessageBox.Show($"Цена тикера {alarm.Ticker} >= {alarm.TargetPrice}");
            else
                MessageBox.Show($"Цена тикера {alarm.Ticker} < {alarm.TargetPrice}");
        }

        private void showReports_Click(object sender, RoutedEventArgs e)
        {
            CompanyData selected = (CompanyData)Watchlist.SelectedItem;
            vm.ShowReportsWindow(selected);
        }

        private void showFacts_Click(object sender, RoutedEventArgs e)
        {
            CompanyData selected = (CompanyData)Watchlist.SelectedItem;
            vm.ShowFactsWindow(selected);
        }

        private void showAlarms_Click(object sender, RoutedEventArgs e)
        {
            CompanyData selected = (CompanyData)Watchlist.SelectedItem;
            vm.ShowAlarmsWindow(selected);
        }
    }
}
