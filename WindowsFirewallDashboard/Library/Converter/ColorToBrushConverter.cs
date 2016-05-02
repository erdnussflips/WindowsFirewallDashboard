using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WindowsFirewallDashboard.Library.Converter
{
	public class ColorToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Color)
			{
				return new SolidColorBrush((Color)value);
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is SolidColorBrush)
			{
				return (value as SolidColorBrush).Color;
			}

			return null;
		}
	}
}
