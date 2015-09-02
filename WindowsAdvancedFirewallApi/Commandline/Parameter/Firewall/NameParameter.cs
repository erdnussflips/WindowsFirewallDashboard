using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public class NameParameter : RuleParameter<NameParameter, NameParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Custom(string ruleName)
			{
				return new Value { Value = ruleName };
			}
		}

		public NameParameter(Value value) : base("name", value) { }
		public NameParameter() : this(null) { }
	}
}
