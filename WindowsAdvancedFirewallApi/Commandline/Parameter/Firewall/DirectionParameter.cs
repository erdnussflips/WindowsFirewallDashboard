using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public class DirectionParameter : RuleParameter<DirectionParameter, DirectionParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Inbound = new Value { Value = "in" };
			public static Value Outbound = new Value { Value = "out" };
		}

		public DirectionParameter(Value value) : base("dir", value) { }
		public DirectionParameter() : this(null) { }
	}
}
