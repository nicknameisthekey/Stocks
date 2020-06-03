using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Stocks
{
    public class ValueToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float v)
            {
                if (v < 0)
                    return new SolidColorBrush(Colors.Red);
                else return new SolidColorBrush(Colors.Green);
            }
            else return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
