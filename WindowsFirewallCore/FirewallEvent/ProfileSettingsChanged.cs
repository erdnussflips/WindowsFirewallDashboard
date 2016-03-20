using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.FirewallEvent
{
	[Serializable]
	public class ProfileSettingsChanged : ASettings
	{
		// EventID: 2003

		public int Profiles;

		public string SettingValueString;

	}
}
