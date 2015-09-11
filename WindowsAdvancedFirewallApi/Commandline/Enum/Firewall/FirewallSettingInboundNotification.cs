using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallSettingInboundNotification : NetshControllable<FirewallSettingInboundNotification>
	{
		public const string Name = "inboundusernotification";

		public static FirewallSettingInboundNotification Default = Enable;
		public static FirewallSettingInboundNotification DefaultForServer2008 = Disable;
		public static FirewallSettingInboundNotification DefaultForGPO = NotConfigured;
	}
}
