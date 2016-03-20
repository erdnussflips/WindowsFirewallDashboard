using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallLibrary.FirewallEvent
{
	[Serializable]
	public class ProfileSettingsChanged : ASettings
	{
		// EventID: 2003

		public int Profiles;

		public string SettingValueString;

	}
}
