using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public class ActionParameter : RuleParameter<ActionParameter, ActionParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Allow = new Value { Value = "allow" };
			public static Value Block = new Value { Value = "block" };
			public static Value Bypass = new Value { Value = "bypass" };
		}

		public ActionParameter(Value value) : base("action", value) { }
		public ActionParameter() : this(null) { }
	}
}
