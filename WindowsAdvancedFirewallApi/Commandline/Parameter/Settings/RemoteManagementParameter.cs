using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Settings
{
	public class RemoteManagementParameter : SettingsParameter<RemoteManagementParameter, RemoteManagementParameter.Value>
	{
		public class Value : NetshControllableValue<Value>
		{
			public static readonly Value Default = Disable;
			public static readonly Value DefaultForGPO = NotConfigured;
		}

		public RemoteManagementParameter(Value value) : base("remotemanagement", value) { }
		public RemoteManagementParameter() : this(null) { }
	}
}
