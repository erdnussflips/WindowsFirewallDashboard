using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class RemoteIpAddressParameter : RuleMultipleValueParameter<RemoteIpAddressParameter, RemoteIpAddressParameter.Value>
	{
		public class Value : NetshAnyValue<Value>
		{
			public static readonly Value Default = Any;

			public static readonly Value LocalSubnet = new Value { Value = "localsubnet" };
			public static readonly Value DNS = new Value { Value = "dns" };
			public static readonly Value DHCP = new Value { Value = "dhcp" };
			public static readonly Value WINS = new Value { Value = "wins" };
			public static readonly Value StandardGateway = new Value { Value = "defaultgateway" };

			public static Value Custom(string address)
			{
				return new Value { Value = address };
			}
		}

		public RemoteIpAddressParameter(params Value[] values) : base("remoteip", values) { }
		public RemoteIpAddressParameter() : this(null) { }
	}
}
