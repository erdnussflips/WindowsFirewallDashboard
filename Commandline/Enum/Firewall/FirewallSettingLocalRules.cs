using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallSettingLocalRules : NetshControllable<FirewallSettingLocalRules>
	{
		public const string Name = "localfirewallrules";

		public static FirewallSettingLocalRules Default = Enable;
		public static FirewallSettingLocalRules DefaultForGPO = NotConfigured;
	}
}
