using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsFirewallDashboard.Library.Utils;
using WindowsFirewallDashboard.Model;
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

		public UserSettings User { get; private set; }

		public InstallManager Installer { get; private set; }

		public FirewallManager Firewall { get; private set; }
		public NotificationManager Notifications { get; private set; }
		public TrayManager Tray { get; private set; }

		private ApplicationManager()
		{
			Installer = new InstallManager();
			Notifications = new NotificationManager();
			Tray = new TrayManager();
			Firewall = new FirewallManager(Notifications);

			Tray.Icon = new Icon(Application.GetResourceStream(ApplicationResource.NotifyIconWhite).Stream);

			LocalizationTool.Initialize();
		}

		#region Application Lifecycle
		public void OnStart()
		{
			Load();

			Firewall.StartEventListening();
			Tray.ShowIcon();

			if (User.CheckForUpdatesAutomatically)
			{
				ApplicationUpdater.Instance.CheckForUpdates();
			}

		}

		public void OnActivate()
		{

		}

		public void OnDeactivate()
		{

		}

		public void OnExit()
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
		#endregion

		#region Application General methods
		private void Load()
		{
			User = new UserSettings();
		}

		public void ExitApplication()
		{
			Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
				Application.Current.Shutdown();
			}));
		}
		#endregion
	}
}
