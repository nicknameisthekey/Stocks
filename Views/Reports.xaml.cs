using Stocks.Models;
using Stocks.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Stocks.Views
{
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();

            DataContextChanged += (o, e) => typeCB.SelectedIndex = 0;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ((ReportsVM)DataContext).OpenReport(((InterfaxData)((Hyperlink)sender).DataContext).Link);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DictionaryEntry entry = (DictionaryEntry)e.AddedItems[0];
            var context = (ReportsVM)DataContext;
            context.onSelectedTypeChange(int.Parse((string)entry.Key));
        }
    }
}
