using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Settings
{
	public class LocalConsecRulesParameter : SettingsParameter<LocalConsecRulesParameter, LocalConsecRulesParameter.Value>
	{
		public class Value : NetshControllableValue<Value>
		{
			public static readonly Value Default = Enable;
			public static readonly Value DefaultForGPO = NotConfigured;
		}

		public LocalConsecRulesParameter(Value value) : base("localconsecrules", value) { }
		public LocalConsecRulesParameter() : this(null) { }
	}
}
