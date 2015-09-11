using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Settings
{
	public class InboundUserNotificationParameter : SettingsParameter<InboundUserNotificationParameter, InboundUserNotificationParameter.Value>
	{
		public class Value : NetshControllableValue<Value>
		{
			public static Value Default = Enable;
			public static Value DefaultForServer2008 = Disable;
			public static Value DefaultForGPO = NotConfigured;
		}

		public InboundUserNotificationParameter(Value value) : base("inboundusernotification", value) { }
		public InboundUserNotificationParameter() : this(null) { }
	}
}
