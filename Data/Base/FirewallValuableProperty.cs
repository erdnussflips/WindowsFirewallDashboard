using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Library;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data.Base
{
	public abstract class FirewallValuableProperty<TValueType> : IComparable where TValueType : IComparable
	{
		protected List<IValuable<TValueType>> Values = new List<IValuable<TValueType>>();

		public override string ToString()
		{
			return Values.Stringify();
		}

		public int CompareTo(object obj)
		{
			return ToString().CompareTo(obj?.ToString());
		}
	}

	internal static class FirewallValuablePropertyUtils
	{
		public static IEnumerable<IValuable<TValueType>> ToFirewallValuableProperty<TFirewallValuableProperty, TValueType>(this string value, IValuableFactory<TValueType> factory)
			where TValueType : IComparable
			where TFirewallValuableProperty : FirewallValuableProperty<TValueType>
		{
			var trimmedValue = value?.TrimEnd(',');
			var splittedValues = trimmedValue?.Split(',');

			if (splittedValues == null || splittedValues.Length < 1)
			{
				return null;
			}

			var items = splittedValues.Select<string, IValuable<TValueType>>(item =>
			{
				var splittedValueItems = item?.Split('-');
				if (splittedValueItems?.Count() < 1 || splittedValueItems?.Count() > 2)
				{
					return null;
				}

				string item0;
				if (splittedValueItems?.Count() == 1 && factory.ValidateValue(item0 = splittedValueItems[0]))
				{
					return factory.CreateValueSingle(factory.ParseValue(item0));
				}

				string item1;
				if (splittedValueItems?.Count() == 2 && factory.ValidateValue(item0 = splittedValueItems[0]) && factory.ValidateValue(item1 = splittedValueItems[1]))
				{
					return factory.CreateValueRange(factory.ParseValue(item0), factory.ParseValue(item1));
				}

				return null;
			});

			if (items?.Count() > 0 && items?.ElementAt(0) != null)
			{
				return items;
			}

			return null;
		}

		public static string ToNativeValueGeneric<TFirewallValuableProperty, TValueType>(this TFirewallValuableProperty value)
			where TValueType : IComparable
			where TFirewallValuableProperty : FirewallValuableProperty<TValueType>
		{
			return value.ToString();
		}
	}
}
