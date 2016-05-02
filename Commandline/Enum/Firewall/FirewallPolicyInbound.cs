using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallPolicyInbound : NetshConfigurable<FirewallPolicyInbound>
	{
		public static FirewallPolicyInbound BlockInbound = new FirewallPolicyInbound { value = "blockinbound" };
		public static FirewallPolicyInbound BlockInboundAlways = new FirewallPolicyInbound { value = "blockinboundalways" };
		public static FirewallPolicyInbound AllowInbound = new FirewallPolicyInbound { value = "allowinbound" };

		public static FirewallPolicyInbound Default = BlockInbound;
	}
}
