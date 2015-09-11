using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class SecurityParameter : RuleSingleParameter<SecurityParameter, SecurityParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Default = NotRequired;

			public static Value Authenticate = new Value { Value = "authenticate" };
			public static Value AuthenticateEncryption = new Value { Value = "authenc" };
			public static Value AuthenticateDynamicNegotiateEncryption = new Value { Value = "authdynenc" };
			public static Value AuthenticateNoEncapsulation = new Value { Value = "authnoencap" };
			public static Value NotRequired = new Value { Value = "notrequired" };
		}

		public SecurityParameter(Value value) : base("edge", value) { }
		public SecurityParameter() : this(null) { }
	}
}
