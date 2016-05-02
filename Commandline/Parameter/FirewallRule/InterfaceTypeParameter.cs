using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class InterfaceTypeParameter : RuleSingleParameter<InterfaceTypeParameter, InterfaceTypeParameter.Value>
	{
		public class Value : NetshAnyValue<Value>
		{
			public static Value Default = Any;

			public static Value Wireless = new Value { Value = "wireless" };
			public static Value LAN = new Value { Value = "lan" };
			public static Value RAS = new Value { Value = "ras" };
		}

		public InterfaceTypeParameter(Value value) : base("interfacetype", value) { }
		public InterfaceTypeParameter() : this(null) { }
	}
}
