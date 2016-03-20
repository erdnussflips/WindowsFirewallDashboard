using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.FirewallEvent
{
	[Serializable]
	public class NoUserNotificationRaised
	{
		// EventID: 2011

		public int ReasonCode;

		public string ApplicationPath;

		public int IPVersion;

		public int Protocol;

		public int Port;

		public int ProcessId;

		public string ModifyingUser;

	}
}
