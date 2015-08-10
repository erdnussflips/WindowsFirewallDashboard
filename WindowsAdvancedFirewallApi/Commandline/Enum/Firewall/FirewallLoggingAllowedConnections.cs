using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallLoggingAllowedConnections : NetshControllable<FirewallLoggingAllowedConnections>
	{
		public const string Name = "allowedconnections";

        public static FirewallLoggingAllowedConnections Default = Disable;
		public static FirewallLoggingAllowedConnections DefaultForGPO = NotConfigured;
	}
}
