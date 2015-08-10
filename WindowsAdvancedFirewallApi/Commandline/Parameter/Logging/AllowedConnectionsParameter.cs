using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Logging
{
	public class AllowedConnectionsParameter : LoggingParameter<AllowedConnectionsParameter, AllowedConnectionsParameter.Value>
	{
		public class Value : NetshControllableValue<Value>
		{
			public static Value Default = Disable;
		}
	}
}
