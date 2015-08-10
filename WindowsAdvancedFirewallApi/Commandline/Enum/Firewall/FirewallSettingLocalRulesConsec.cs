using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallSettingLocalRulesConsec : NetshControllable<FirewallSettingLocalRulesConsec>
	{
		public const string Name = "localconsecrules";

		public static FirewallSettingLocalRulesConsec Default = Enable;
		public static FirewallSettingLocalRulesConsec DefaultForGPO = NotConfigured;
	}
}
