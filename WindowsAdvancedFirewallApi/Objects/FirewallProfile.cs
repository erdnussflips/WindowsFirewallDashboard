using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Objects
{
	public class FirewallProfileObject
	{
		public enum DefaultAction {  }

		public string Name { get; set; }
		public bool Enabled { get; set; }

		/*public __ DefaultInboundAction { get; set; }
		public __ DefaultOutboundAction { get; set; }
		public __ AllowInboundRules { get; set; }
		public __ AllowLocalFirewallRules { get; set; }
		public __ AllowLocalIPsecRules { get; set; }
		public __ AllowUserApps { get; set; }
		public __ AllowUserPorts { get; set; }
		public __ AllowUnicastResponseToMulticast { get; set; }
		public __ NotifyOnListen { get; set; }
		public __ EnableStealthModeForIPsec { get; set; }
		public __ LogFileName { get; set; }
		public __ LogMaxSizeKilobytes { get; set; }
		public __ LogAllowed { get; set; }
		public __ LogBlocked { get; set; }
		public __ LogIgnored { get; set; }
		public __ DisabledInterfaceAliases { get; set; }*/
	}
}
