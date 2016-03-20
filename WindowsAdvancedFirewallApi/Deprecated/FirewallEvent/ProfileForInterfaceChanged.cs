using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallLibrary.FirewallEvent
{
	[Serializable]
	public class ProfileForInterfaceChanged
	{
		// EventID: 2010

		public string InterfaceGuid;

		public string InterfaceName;

		public int OldProfile;

		public int NewProfile;

	}
}
