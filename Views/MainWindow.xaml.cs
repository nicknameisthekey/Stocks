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
        InterfaxVM interfaxVM;
        AlarmsVM alarmsVM;
        public MainWindow()
        {
            SettingsManager.ReadSettings();
            PriceUpdater.Initialize();
            AlarmsChecker.Initialize();
            AlarmsChecker.FirePriceAlarm += showPriceAlarm;
            InitializeComponent();
            vm = new MainVM();
            alarmsVM = new AlarmsVM();
            interfaxVM = new InterfaxVM();
            InterfaxControl.DataContext = interfaxVM;
            AlarmControl.DataContext = alarmsVM;
            DataContext = vm;
        }
        void addNewTicker_Click(object sender, RoutedEventArgs e)
        {
            SearchTicker searchTicker = new SearchTicker();
            searchTicker.Show();
        }

        void deleteTicker_click(object sender, RoutedEventArgs e)
        {
            TickerPrices selected = (TickerPrices)Watchlist.SelectedItem;
            vm.RemoveTicker(selected.Ticker);
        }

        void changeInterfaxId_Click(object sender, RoutedEventArgs e)
        {
            TickerPrices selected = (TickerPrices)Watchlist.SelectedItem;
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
    }
}
