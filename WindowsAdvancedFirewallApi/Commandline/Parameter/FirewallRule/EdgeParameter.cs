using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class EdgeParameter : RuleSingleParameter<EdgeParameter, EdgeParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static readonly Value Yes = new Value { Value = "yes" };
			public static readonly Value No = new Value { Value = "no" };
			public static readonly Value DeferApplication = new Value { Value = "deferapp" };
			public static readonly Value DeferUser = new Value { Value = "deferuser" };
		}

		public EdgeParameter(Value value) : base("edge", value) { }
		public EdgeParameter() : this(null) { }
	}
}
