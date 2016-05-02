using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallLoggingDroppedConnections : NetshControllable<FirewallLoggingDroppedConnections>
	{
		public const string Name = "droppedconnections";

        public static FirewallLoggingDroppedConnections Default = Disable;
		public static FirewallLoggingDroppedConnections DefaultForGPO = NotConfigured;
	}
}
