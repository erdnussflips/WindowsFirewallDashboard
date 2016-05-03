using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Logging
{
	public class DroppedConnectionsParameter : LoggingParameter<DroppedConnectionsParameter, DroppedConnectionsParameter.Value>
	{
		public class Value : NetshControllableValue<Value>
		{
			public static readonly Value Default = Disable;
			public static readonly Value DefaultGPO = NotConfigured;
		}

		public DroppedConnectionsParameter(Value value) : base("droppedconnections", value) { }

		public DroppedConnectionsParameter() : this(null) { }
	}
}
