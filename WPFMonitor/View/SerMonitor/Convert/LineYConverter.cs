using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPFMonitor.View.SerMonitor.Converts
{
    public class LineYConverter : IValueConverter 
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Int32 intValue =System.Convert.ToInt32(value);
            if (intValue == 0)
            {
                return "0";
            }
            else
            {
                return value + "度";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
