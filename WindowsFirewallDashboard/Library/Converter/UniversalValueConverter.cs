using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using NLog;

namespace WindowsFirewallDashboard.Library.Converter
{
	public class UniversalValueConverter : IValueConverter
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// obtain the conveter for the target type
			TypeConverter converter = TypeDescriptor.GetConverter(targetType);

			try
			{
				// determine if the supplied value is of a suitable type
				if (converter.CanConvertFrom(value.GetType()))
				{
					// return the converted value
					return converter.ConvertFrom(value);
				}
				else
				{
					// try to convert from the string representation
					return converter.ConvertFrom(value.ToString());
				}
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// obtain the conveter for the target type
			TypeConverter converter = TypeDescriptor.GetConverter(targetType);

			try
			{
				// determine if the supplied value is of a suitable type
				if (converter.CanConvertTo(value.GetType()))
				{
					// return the converted value
					return converter.ConvertTo(value, targetType);
				}
				else
				{
					// try to convert from the string representation
					return converter.ConvertTo(value.ToString(), targetType);
				}
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return null;
		}
	}
}
