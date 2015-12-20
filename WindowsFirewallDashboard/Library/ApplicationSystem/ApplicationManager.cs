using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
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

			Tray.Icon = new Icon(Application.GetResourceStream(ApplicationResource.NotifyIconWhite).Stream);
		}

		public void Start()
		{
			Firewall.StartEventListening();

			Tray.ShowIcon();

			ApplicationUpdater.Instance.CheckForUpdates();

			Firewall.EventManager.GetEventHistory();
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
