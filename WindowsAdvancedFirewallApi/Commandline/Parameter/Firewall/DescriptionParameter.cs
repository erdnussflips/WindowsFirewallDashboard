using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public class DescriptionParameter : RuleParameter<DescriptionParameter, DescriptionParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Custom(string description)
			{
				return new Value { Value = description };
			}
		}

		public DescriptionParameter(Value value) : base("description", value) { }
		public DescriptionParameter() : this(null) { }
	}
}
