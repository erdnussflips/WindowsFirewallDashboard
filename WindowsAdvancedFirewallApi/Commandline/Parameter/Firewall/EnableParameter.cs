using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public class EnableParameter : RuleSingleParameter<EnableParameter, EnableParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Yes = new Value { Value = "yes" };
			public static Value No = new Value { Value = "no" };
			public static Value Default = Yes;
		}

		public EnableParameter(Value value) : base("dir", value) { }
		public EnableParameter() : this(null) { }
	}
}
