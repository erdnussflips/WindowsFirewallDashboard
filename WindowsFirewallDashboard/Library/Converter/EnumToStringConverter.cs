using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsFirewallDashboard.Library.Converter
{
    class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return EnumUtils.GetEnumValueName(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
