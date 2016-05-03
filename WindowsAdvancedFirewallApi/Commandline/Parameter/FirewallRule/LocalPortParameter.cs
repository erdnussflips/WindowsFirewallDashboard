using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule.Value;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class LocalPortParameter : RuleMultipleValueParameter<LocalPortParameter, LocalPortParameter.Value>
	{
		public class Value : PortValue<Value>
		{
			public static readonly Value Default = Any;

			public static readonly Value RPC = new Value { Value = "rpc" };
			public static readonly Value RPC_EPMAP = new Value { Value = "rpc-epmap" };
			public static readonly Value Teredo = new Value { Value = "teredo" };
			public static readonly Value IP_HTTPS = new Value { Value = "iphttps" };
		}

		public LocalPortParameter(params Value[] values) : base("localport", values) { }
		public LocalPortParameter() : this(null) { }
	}
}
