using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WindowsAdvancedFirewallApi.Data;
using WindowsFirewallDashboard.Library.Utils;

namespace WindowsFirewallDashboard.Library.Converter
{
	public class IListToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IList)
			{
				var list = (value as IList).Cast<object>().ToList();
				var stringBuilder = new StringBuilder();

				for (int i = 0; i < list.Count; i++)
				{
					var item = list.ElementAt(i);
					var isLastItem = i == list.Count - 1;

					stringBuilder.Append(item.ToString());

					if (!isLastItem)
					{
						stringBuilder.Append(", ");
					}
				}

				return stringBuilder.ToString();
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
