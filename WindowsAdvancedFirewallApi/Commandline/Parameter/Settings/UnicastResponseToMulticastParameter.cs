using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Settings
{
	public class UnicastResponseToMulticastParameter : SettingsParameter<UnicastResponseToMulticastParameter, UnicastResponseToMulticastParameter.Value>
	{
		public class Value : NetshControllableValue<Value>
		{
			public static readonly Value Default = Enable;
			public static readonly Value DefaultForGPO = NotConfigured;
		}

		public UnicastResponseToMulticastParameter(Value value) : base("unicastresponsetomulticast", value) { }
		public UnicastResponseToMulticastParameter() : this(null) { }
	}
}
