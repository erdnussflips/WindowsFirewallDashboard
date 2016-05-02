using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class ServiceParameter : RuleSingleParameter<ServiceParameter, ServiceParameter.Value>
	{
		public class Value : NetshAnyValue<Value>
		{
			public static Value Custom(string serviceShortName)
			{
				return new Value { Value = serviceShortName };
			}
		}

		public ServiceParameter(Value value) : base("service", value) { }
		public ServiceParameter() : this(null) { }
	}
}
