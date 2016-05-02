using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallState : NetshParameter
	{
		public static FirewallState On = new FirewallState { value = "on" };
		public static FirewallState Off = new FirewallState { value = "off" };

		public static FirewallState Default = On;
	}
}
