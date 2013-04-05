using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace WPFMonitor.Core.Converter
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private static readonly BooleanToVisibilityConverter _instance = new BooleanToVisibilityConverter();

        public static BooleanToVisibilityConverter Instance { get { return _instance; } }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value);
        }

        private object Convert(object value)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
