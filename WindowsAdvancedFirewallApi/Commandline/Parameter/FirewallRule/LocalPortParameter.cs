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
			public static Value Default = Any;

			public static Value RPC = new Value { Value = "rpc" };
			public static Value RPC_EPMAP = new Value { Value = "rpc-epmap" };
			public static Value Teredo = new Value { Value = "teredo" };
			public static Value IP_HTTPS = new Value { Value = "iphttps" };
		}

		public LocalPortParameter(params Value[] values) : base("localport", values) { }
		public LocalPortParameter() : this(null) { }
	}
}
