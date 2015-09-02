using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public class LocalIpAddressParameter : RuleParameter<LocalIpAddressParameter, LocalIpAddressParameter.Value>
	{
		public class Value : NetshAnyValue<Value>
		{
			public static Value Default = Any;

			public static Value Custom(string serviceShortName)
			{
				return new Value { Value = serviceShortName };
			}
		}

		//public LocalIpAddressParameter(List<Value> value) : base("localip", value) { }
		public LocalIpAddressParameter(Value value) : base("localip", value) { }
		public LocalIpAddressParameter() : this(null) { }
	}
}
