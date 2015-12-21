using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallUserNotificationEvent : FirewallBaseObject
	{
		public int ReasonCode { get; set; }
		public string ApplicationPath { get; set; }
		public int IpVersion { get; set; }
		public int Protocol { get; set; }
		public int Port { get; set; }
		public int ProcessId { get; set; }
	}
}
