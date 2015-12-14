using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;

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
			Notifications = new NotificationManager();
			Tray = new TrayManager();
			Firewall = new FirewallManager(Notifications);

			Tray.Icon = new System.Drawing.Icon(Application.GetResourceStream(ApplicationResource.NotifyIconGreen).Stream);
		}

		public void Start()
		{
			Firewall.StartEventListening();
		}

		public void Activate()
		{

		}

		public void Deactivate()
		{

		}

		public void Exit()
		{
			Firewall.StopEventListening();
		}

		public void StartInBackground()
		{

		}

		public void StopInBackground()
		{

		}
	}
}
