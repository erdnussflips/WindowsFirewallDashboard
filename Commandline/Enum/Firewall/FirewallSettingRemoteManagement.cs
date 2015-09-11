using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallSettingRemoteManagement : NetshControllable<FirewallSettingRemoteManagement>
	{
		public const string Name = "remotemanagement";

		public static FirewallSettingRemoteManagement Default = Disable;
		public static FirewallSettingRemoteManagement DefaultForGPO = NotConfigured;
	}
}
