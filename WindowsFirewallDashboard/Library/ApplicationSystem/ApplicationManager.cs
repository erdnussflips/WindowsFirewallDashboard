using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsFirewallDashboard.Library.Utils;
using WindowsFirewallDashboard.Resources.Localization;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	#pragma warning disable CC0091
	class ApplicationManager
	{
		private static ApplicationManager _singleton;
		protected static ApplicationManager Singleton
		{
			get
			{
				if (_singleton == null)
				{
					_singleton = new ApplicationManager();
				}
				return _singleton;
			}
		}

		public static ApplicationManager Instance => Singleton;

		public FirewallManager Firewall { get; private set; }
		public NotificationManager Notifications { get; private set; }
		public TrayManager Tray { get; private set; }

		private ApplicationManager()
		{
			Notifications = new NotificationManager();
			Tray = new TrayManager();
			Firewall = new FirewallManager(Notifications);

			Tray.Icon = new Icon(Application.GetResourceStream(ApplicationResource.NotifyIconWhite).Stream);

			LocalizationTool.Initialize();
		}

		public void Start()
		{
			Firewall.StartEventListening();

			Tray.ShowIcon();

			ApplicationUpdater.Instance.CheckForUpdates();
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
			Tray.HideIcon();
		}

		public void StartInBackground()
		{

		}

		public void StopInBackground()
		{

		}
	}
}
