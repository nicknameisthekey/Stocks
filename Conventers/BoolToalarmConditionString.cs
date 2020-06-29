using System;
using System.Globalization;
using System.Windows.Data;

namespace Stocks.Conventers
{
    public class BoolToalarmConditionString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                if (b)
                    return "Цена >= ";
                else return "Цена < ";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
