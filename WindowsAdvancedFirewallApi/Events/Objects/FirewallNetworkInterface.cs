using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallNetworkInterface : FirewallBaseObject
	{
		public string InterfaceGUID { get; set; }
		public string InterfaceName { get; set; }
		public Profile OldProfile { get; set; }
		public Profile NewProfile { get; set; }
	}
}
