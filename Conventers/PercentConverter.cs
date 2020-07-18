using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Stocks
{
    class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            if (value is float f)
            {
                if (f >= 0)
                    result += "+";
                else
                    result += "";
                result += $"{f.ToString("0.0#")}%";
                return result;
            }
            if (value is int i)
                return $"{i}%";
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
