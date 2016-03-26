using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallNetworkInterfaceProfile : FirewallBaseObject
	{
		public string InterfaceGUID { get; set; }
		public string InterfaceName { get; set; }
		public FirewallProfileType OldProfile { get; set; }
		public FirewallProfileType NewProfile { get; set; }
	}
}
