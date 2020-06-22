using Stocks.Models;
using System.Windows;
using System.Windows.Controls;

namespace Stocks.Views
{
    public partial class AddAlarm : Window
    {
        string ticker;
        public AddAlarm(string ticker)
        {
            this.ticker = ticker;
            InitializeComponent();
        }

        private void addAlarm_Click(object sender, RoutedEventArgs e)
        {
            bool condition = false;
            TextBlock selected = (TextBlock)ConditionBox.SelectedItem;
            if (selected.Text == "больше или равно")
                condition = true;
            PriceAlarm alarm = new PriceAlarm(ticker, float.Parse(priceTB.Text), condition);
            SettingsManager.AddPriceAlarm(alarm);
            Close();
        }
    }
}
