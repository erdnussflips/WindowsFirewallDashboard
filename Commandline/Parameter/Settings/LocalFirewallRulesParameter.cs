using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Settings
{
	public class LocalFirewallRulesParameter : SettingsParameter<LocalFirewallRulesParameter, LocalFirewallRulesParameter.Value>
	{
		public class Value : NetshControllableValue<Value>
		{
			public static Value Default = Enable;
			public static Value DefaultForGPO = NotConfigured;
		}

		public LocalFirewallRulesParameter(Value value) : base("localfirewallrules", value) { }
		public LocalFirewallRulesParameter() : this(null) { }
	}
}
