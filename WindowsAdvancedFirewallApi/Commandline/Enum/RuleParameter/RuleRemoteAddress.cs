using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.RuleParameter
{
	public class RuleRemoteAddress : RuleAddress<RuleRemoteAddress>
	{
		public static RuleRemoteAddress LocalSubnet = new RuleRemoteAddress { value = "localsubnet" };
		public static RuleRemoteAddress Dns = new RuleRemoteAddress { value = "dns" };
		public static RuleRemoteAddress Dhcp = new RuleRemoteAddress { value = "dhcp" };
		public static RuleRemoteAddress Wins = new RuleRemoteAddress { value = "wins" };
		public static RuleRemoteAddress DefaultGateway = new RuleRemoteAddress { value = "defaultgateway" };
	}
}
