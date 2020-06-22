using Stocks.Models;
using Stocks.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Stocks.Views
{
    public partial class Alarms : UserControl
    {
        public Alarms()
        {
            InitializeComponent();
        }

        void addAlarm_Click(object sender, RoutedEventArgs e)
        {
            var vm = (AlarmsVM)DataContext;
            vm.onAddNewAlarm();
        }

        private void removeAlarm_Click(object sender, RoutedEventArgs e)
        {
            PriceAlarm alarm = (PriceAlarm)((Button)sender).DataContext;
            var vm = (AlarmsVM)DataContext;
            vm.onRemoveAlarm(alarm);
        }
    }
}
