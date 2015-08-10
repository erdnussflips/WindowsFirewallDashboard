using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallSettingUnicastResponseToMulticast : NetshControllable<FirewallSettingUnicastResponseToMulticast>
	{
		public const string Name = "unicastresponsetomulticast";

		public static FirewallSettingUnicastResponseToMulticast Default = Enable;
		public static FirewallSettingUnicastResponseToMulticast DefaultForGPO = NotConfigured;
	}
}
