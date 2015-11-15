using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events;

namespace WindowsFirewallDashboard.ApplicationSystem
{
	public class FirewallManager
	{
		public FirewallEventManager EventManager
		{
			get
			{
				return FirewallEventManager.Instance;
			}
		}
	}
}
