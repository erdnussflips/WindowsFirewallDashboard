using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.FirewallEvent
{
	[Serializable]
	public class SettingsChanged : ASettings
	{
		// EventID: 2002

		public string SettingValueDisplay;

	}
}
