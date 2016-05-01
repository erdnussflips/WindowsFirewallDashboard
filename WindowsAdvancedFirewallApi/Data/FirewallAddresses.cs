using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data.Generics;
using WindowsAdvancedFirewallApi.Library;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public class FirewallAddresses : FirewallValuableProperty
	{
		private List<IValuable> Addresses => Values;

		public FirewallAddresses(params IValuable[] addresses)
		{
			Addresses.AddRange(addresses);
		}
	}

	internal static class FirewallAddressesUtils
	{
		class Factory : IValuableFactory<string>
		{
			public IValuableRange<string> CreateValueRange(string lowest, string highest)
			{
				throw new NotImplementedException();
			}

			public IValuableSingle<string> CreateValueSingle(string value)
			{
				throw new NotImplementedException();
			}

			public string ParseValue(string value)
			{
				throw new NotImplementedException();
			}

			public bool ValidateValue(string value)
			{
				throw new NotImplementedException();
			}
		}

		public static FirewallAddresses ToFirewallAddresses(this string value)
		{
			var result = value.ToFirewallValuableProperty<FirewallAddresses, string>(new Factory());

			if (result != null)
			{
				return new FirewallAddresses(result.ToArray());
			}

			var trimmedValue = value?.TrimEnd(',');
			var splittedPortValues = trimmedValue?.Split(',');

			if (splittedPortValues == null || splittedPortValues.Length < 1)
			{
				return null;
			}

			return null;
		}

		public static string ToNativeValue(this FirewallAddresses addresses)
		{
			return addresses.ToNativeValueGeneric();
		}
	}
}
