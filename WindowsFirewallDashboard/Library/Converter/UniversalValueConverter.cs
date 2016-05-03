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
			var converter = TypeDescriptor.GetConverter(targetType);

			try
			{
				// determine if the supplied value is of a suitable type
				return converter.CanConvertFrom(value.GetType()) ? converter.ConvertFrom(value) : converter.ConvertFrom(value.ToString());
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
			var converter = TypeDescriptor.GetConverter(targetType);

			try
			{
				// determine if the supplied value is of a suitable type
				return converter.CanConvertTo(value.GetType()) ? converter.ConvertTo(value, targetType) : converter.ConvertTo(value.ToString(), targetType);
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return null;
		}
	}
}
