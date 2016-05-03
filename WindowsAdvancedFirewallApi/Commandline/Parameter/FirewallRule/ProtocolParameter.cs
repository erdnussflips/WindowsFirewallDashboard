using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class ProtocolParameter : RuleSingleParameter<ProtocolParameter, ProtocolParameter.Value>
	{
		public class Value : NetshAnyValue<Value>
		{
			public static readonly Value Default = Any;

			public static readonly Value ICMPv4 = new Value { Value = "icmpv4" };
			public static readonly Value ICMPv6 = new Value { Value = "icmpv6" };
			public static Value ICMPv4_Custom(int type, int code) => ICMP_Custom(ICMPv4.Value, type, code);
			public static Value ICMPv6_Custom(int type, int code) => ICMP_Custom(ICMPv6.Value, type, code);
			public static readonly Value TCP = new Value { Value = "tcp" };
			public static readonly Value UDP = new Value { Value = "udp" };

			private static void checkRange(int number)
			{
				if (number < 0 || number > 255)
				{
					throw new ArgumentOutOfRangeException(nameof(number), "The number must between 0 and 255.");
				}
			}

			public static Value Custom(int number)
			{
				checkRange(number);
				return new Value { Value = number.ToString() };
			}

			private static Value ICMP_Custom(string value, int type, int code)
			{
				checkRange(type); checkRange(code);
				return new Value { Value = value + ":" + type + "," + code };
			}
		}

		public ProtocolParameter(Value value) : base("protocol", value) { }
		public ProtocolParameter() : this(null) { }
	}
}
