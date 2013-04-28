using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPFMonitor.View.SerMonitor.Converts
{
    public class CheckBoxConvert : IValueConverter
    {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if ((int)value > 0)
            //    return 1;
            //else if ((int)value == 0)
            //    return 0;
            //else
            //    return -1;
            Boolean bvalue = (Boolean)value;
            if (bvalue)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
