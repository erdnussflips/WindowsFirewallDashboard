using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallPolicyOutbound : NetshConfigurable<FirewallPolicyOutbound>
	{
		public static FirewallPolicyOutbound BlockOutbound = new FirewallPolicyOutbound { value = "blockoutbound" };
		public static FirewallPolicyOutbound AllowOutbound = new FirewallPolicyOutbound { value = "allowoutbound" };

		public static FirewallPolicyOutbound Default = AllowOutbound;
	}
}
