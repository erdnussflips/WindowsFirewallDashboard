using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Model
{
	[Serializable]
	class UserSettings
	{
		public bool CheckForUpdatesAutomatically = true;
		public bool InstallUpdatesAutomatically = true;
		public bool NotifyForNewNetworkAccess = true;
	}
}
