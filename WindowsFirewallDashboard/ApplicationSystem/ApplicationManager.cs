using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events;

namespace WindowsFirewallDashboard.ApplicationSystem
{
	public class ApplicationManager
	{
		private static ApplicationManager _instance;

		public static ApplicationManager Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = new ApplicationManager();
				}
				return _instance;
			}
		}

		public FirewallManager Firewall { get; private set; }
		public NotificationManager Notifications { get; private set; }
		public TrayManager Tray { get; private set; }

		private ApplicationManager()
		{
			Firewall = new FirewallManager();
			Notifications = new NotificationManager();
			Tray = new TrayManager();
		}

		public void Start()
		{
			Firewall.EventManager.SettingsChanged += EventManager_SettingsChanged;
			Firewall.EventManager.StartListingFirewall();
		}

		private void EventManager_SettingsChanged(object sender, WindowsAdvancedFirewallApi.Events.Arguments.FirewallSettingEventArgs e)
		{
			Notifications.ShowWindow();
		}

		public void Activate()
		{

		}

		public void Deactivate()
		{

		}

		public void Exit()
		{
			Firewall.EventManager.StopListingFirewall();
		}

		public void StartInBackground()
		{

		}

		public void StopInBackground()
		{

		}
	}
}
