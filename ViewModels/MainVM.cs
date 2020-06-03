using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Stocks.ViewModels
{
    public class MainVM : BaseVM
    {
        private ObservableCollection<Price> prices;

        public ObservableCollection<Price> Prices
        {
            get => prices;
            set { prices = value; onPropertyChange(); }
        }

        public MainVM(List<Price> prices)
        {
            Prices = new ObservableCollection<Price>(prices);
        }


    }
}
