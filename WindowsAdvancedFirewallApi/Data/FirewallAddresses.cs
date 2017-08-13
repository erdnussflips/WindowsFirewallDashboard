using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data.Base;
using WindowsAdvancedFirewallApi.Library;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public class FirewallAddresses
	{
		public enum Modes
		{
			Any, Specific
		}

		public class Value
		{
			public enum ValueTypes
			{
				Unknown, Custom, LocalSubnet, DNS, DHCP, WINS, DefaultGateway, IntrAnet, IntErnet, Ply2Renders, RmtIntrAnet
			}

			public ValueTypes Type { private set; get; }
			public string Native { private set; get; }
			public IValuable<IPAddress> Address { private set; get; }

			private Value(ValueTypes type, string native)
			{
				Type = type;
				Native = native;
			}

			public Value(IValuable<IPAddress> address)
			{
				Type = ValueTypes.Custom;
				Address = address;
			}

			public override string ToString()
			{
				if (Type == ValueTypes.Custom)
				{
					return Address.ToString();
				}

				return Type.ToString();
			}

			public static readonly Value LocalSubnet = new Value(ValueTypes.LocalSubnet, "LocalSubnet");
			public static readonly Value DNS = new Value(ValueTypes.DNS, "DNS");
			public static readonly Value DHCP = new Value(ValueTypes.DHCP, "DHCP");
			public static readonly Value WINS = new Value(ValueTypes.WINS, "WINS");
			public static readonly Value DefaultGateway = new Value(ValueTypes.DefaultGateway, "DefaultGateway");
			public static readonly Value IntrAnet = new Value(ValueTypes.IntrAnet, "IntrAnet");
			public static readonly Value IntErnet = new Value(ValueTypes.IntErnet, "IntErnet");
			public static readonly Value Ply2Renders = new Value(ValueTypes.Ply2Renders, "Ply2Renders");
			public static readonly Value RmtIntrAnet = new Value(ValueTypes.RmtIntrAnet, "RmtIntrAnet");

			public static readonly IList<Value> Predefined = new List<Value> { LocalSubnet, DNS, DHCP, WINS, DefaultGateway, IntrAnet, IntErnet, Ply2Renders, RmtIntrAnet };
		}

		public Modes Mode { private set; get; }
		public string Native { private set; get; }

		public IList<Value> Addresses { private set; get; } = new List<Value>();

		private FirewallAddresses(Modes mode, string native)
		{
			Mode = mode;
			Native = native;
		}

		public FirewallAddresses(params Value[] addresses)
		{
			Mode = Modes.Specific;

			if (addresses != null)
			{
				Addresses.AddRange(addresses);
			}
		}

		public override string ToString()
		{
			if (Mode == Modes.Any)
			{
				return Native;
			}

			return Addresses.Stringify();
		}

		public static readonly FirewallAddresses Any = new FirewallAddresses(Modes.Any, "*");
	}

	static class FirewallAddressesUtils
	{
		class Factory : IValuableFactory<IPAddress>
		{
			public IValuableRange<IPAddress> CreateValueRange(IPAddress lowest, IPAddress highest)
			{
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

		static FirewallAddresses.Value GetPredefinedValue(string value)
		{
			foreach (var item in FirewallAddresses.Value.Predefined)
			{
				if (value?.Equals(item.Native) ?? false)
				{
					return item;
				}
			}

			return null;
		}

		public static FirewallAddresses ToFirewallAddresses(this string value)
		{
			return value?.ToFirewallAddresses(null);
		}

		public static FirewallAddresses ToFirewallAddresses(this string value, FirewallAddresses existing)
		{
			if (existing?.Mode == FirewallAddresses.Modes.Any)
			{
				return existing;
			}

			var trimmedValue = value?.Trim().TrimEnd(',');

			// any mode
			if (trimmedValue.Equals(FirewallAddresses.Any.Native))
			{
				return FirewallAddresses.Any;
			}

			// custom mode
			var splittedValue = value?.Split(',');
			var firewallAddress = existing ?? new FirewallAddresses();

			foreach (var item in splittedValue)
			{
				var addressValue = GetPredefinedValue(item);

				if (addressValue == null)
				{
					addressValue = new FirewallAddresses.Value(item.ParseValue(new Factory()));
				}

				firewallAddress.Addresses.Add(addressValue);
			}

			return firewallAddress;
		}

		public static string ToNativeValue(this FirewallAddresses addresses)
		{
			return addresses.ToString();
		}
	}
}
