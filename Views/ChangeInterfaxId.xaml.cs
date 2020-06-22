using System.Windows;

namespace Stocks.Views
{
    public partial class ChangeInterfaxId : Window
    {
        string ticker;
        public ChangeInterfaxId(string ticker)
        {
            InitializeComponent();
            this.ticker = ticker;
        }
        void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsManager.ChangeInterfaxId(ticker, IdTextbox.Text.Trim());
        }
    }
}
