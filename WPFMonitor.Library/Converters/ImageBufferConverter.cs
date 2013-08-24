using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MonitorSystem.Converters
{
    public class ImageBufferConverter : IValueConverter
    {
        private readonly static ImageBufferConverter _instance = new ImageBufferConverter();

        public static ImageBufferConverter Instance { get { return _instance; } }
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
            var img = new BitmapImage();
            if (value is byte[])
            {
				if ((value as byte[]).Length > 0)
				{
					img.BeginInit();

					img.StreamSource = new MemoryStream((byte[])value);

					img.EndInit();
				}
            }
            return img;
        }
    }
}
