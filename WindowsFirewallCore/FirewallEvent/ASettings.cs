using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.FirewallEvent
{
	[Serializable]
	public abstract class ASettings
	{
		// EventID: 2002, 2003

		public int SettingType;

		public int SettingValueSize;

		public string SettingValue;

		public int Origin;

		public string ModifyingUser;

		public string ModifyingApplication;
	}
}
