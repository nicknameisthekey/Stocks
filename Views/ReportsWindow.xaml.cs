using Stocks.Models;
using Stocks.ViewModels;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Stocks.Views
{
    public partial class ReportsWindow : Window
    {
        public ReportsWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            ((ReportsVM)DataContext).OpenReport(((InterfaxData)((Hyperlink)sender).DataContext).Link);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DictionaryEntry entry = (DictionaryEntry)e.AddedItems[0];
            var context = (ReportsVM)DataContext;
            context.OnSelectedTypeChange(int.Parse((string)entry.Key));
        }
    }
}
