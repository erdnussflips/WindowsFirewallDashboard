using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class NameParameter : RuleSingleParameter<NameParameter, NameParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Custom(string ruleName)
			{
				if(string.Compare(ruleName, "all", true) != 0)
				{
					throw new ArgumentOutOfRangeException(nameof(ruleName), "The value 'all' is permitted.");
				}

				return new Value { Value = ruleName };
			}
		}

		public NameParameter(Value value) : base("name", value) { }
		public NameParameter() : this(null) { }
	}
}
