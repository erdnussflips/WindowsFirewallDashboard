using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data.BaseObjects;
using WindowsAdvancedFirewallApi.Library;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public class FirewallAddresses : FirewallValuableProperty<IPAddress>
	{
		public enum Types
		{
			Unknown, All, Specific
		}

		public Types Type { private set; get; }
		public string Native { private set; get; }

		public List<IValuable<IPAddress>> Addresses => Values;

		private FirewallAddresses(Types type, string native)
		{
			Type = type;
			Native = native;
		}

		public FirewallAddresses(params IValuable<IPAddress>[] addresses)
		{
			Type = Types.Specific;
			Addresses.AddRange(addresses);
		}

		public override string ToString()
		{
			if (Type == Types.Specific)
			{
				return base.ToString();
			}

			return Type.ToString();
		}

		// all addresses
		public static readonly FirewallAddresses All = new FirewallAddresses(Types.All, "*");

		public static readonly IList<FirewallAddresses> Predefined = new List<FirewallAddresses> { All };
	}

	internal static class FirewallAddressesUtils
	{
		class Factory : IValuableFactory<IPAddress>
		{
			public IValuableRange<IPAddress> CreateValueRange(IPAddress lowest, IPAddress highest)
			{
				lowest.GetAddressValue();
				return new IPAddressValueRange(lowest, highest);
			}

			public IValuableSingle<IPAddress> CreateValueSingle(IPAddress value)
			{
				return new IPAddressValue(value);
			}

			public IPAddress ParseValue(string value)
			{
				return IPAddressUtils.Parse(value);
			}

			public bool ValidateValue(string value)
			{
				return IPAddressUtils.IsValid(value);
			}
		}

		public static FirewallAddresses ToFirewallAddresses(this string value)
		{
			var result = value.ToFirewallValuableProperty<FirewallAddresses, IPAddress>(new Factory());

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

			foreach (var item in FirewallAddresses.Predefined)
			{
				if (trimmedValue?.Equals(item.Native) ?? false)
				{
					return item;
				}
			}

			return null;
		}

		public static string ToNativeValue(this FirewallAddresses addresses)
		{
			return addresses.ToNativeValueGeneric<FirewallAddresses, IPAddress>();
		}
	}
}
