using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class EnableParameter : RuleSingleParameter<EnableParameter, EnableParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static readonly Value Yes = new Value { Value = "yes" };
			public static readonly Value No = new Value { Value = "no" };
			public static readonly Value Default = Yes;
		}

		public EnableParameter(Value value) : base("dir", value) { }
		public EnableParameter() : this(null) { }
	}
}
