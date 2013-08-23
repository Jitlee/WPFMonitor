using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace MonitorSystem.Controls.Converters
{
    public class FontFamilyConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string str = ((string)value).Trim();
                str = Common1.GetFontEn(str);
				try
				{
                    return new FontFamily(str);
				}
				catch (FormatException exception)
				{
                    throw new FormatException(string.Format("Unable to convert {0} - {1}", (string)value, "FontFamily"), exception);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
