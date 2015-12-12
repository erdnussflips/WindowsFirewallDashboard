using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;

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

		private NotificationManager _notifications;

		public FirewallManager(NotificationManager notifications)
		{
			_notifications = notifications;
		}

		public void StartEventListening()
		{
			EventManager.StartListingFirewall();
			EventManager.SettingsChanged += EventManager_SettingsChanged;
		}


		public void StopEventListening()
		{
			EventManager.StopListingFirewall();
		}

		private void EventManager_SettingsChanged(object sender, FirewallSettingEventArgs e)
		{
			_notifications.ShowEventNotification();
		}
	}
}
