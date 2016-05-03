using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule.Value;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class RemotePortParameter : RuleMultipleValueParameter<RemotePortParameter, RemotePortParameter.Value>
	{
		public class Value : PortValue<Value>
		{
			public static readonly Value Default = Any;
		}

		public RemotePortParameter(params Value[] values) : base("remoteport", values) { }
		public RemotePortParameter() : this(null) { }
	}
}
