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
		#region Singleton
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
		#endregion

		public UserSettings User { get; private set; }

		public InstallManager Installer { get; private set; }
		public ApplicationUpdater Updater { get; private set; }

		public FirewallManager Firewall { get; private set; }
		public NotificationManager Notifications { get; private set; }
		public WindowManager WindowManager { get; private set; }

		private ApplicationManager()
		{
			Installer = new InstallManager();
			Updater = new ApplicationUpdater();
			WindowManager = new WindowManager();
			Notifications = new NotificationManager();
			Firewall = new FirewallManager(Notifications);

			WindowManager.TrayIcon = new Icon(Application.GetResourceStream(ApplicationResource.NotifyIconWhite).Stream);

			LocalizationTool.Initialize();
		}

		#region Application lifecycle
		public void OnStart()
		{
			Load();

			Firewall.StartEventListening();
			WindowManager.ShowTray();

			if (User.CheckForUpdatesAutomatically)
			{
				Updater.CheckForUpdates();
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
			WindowManager.HideTray();
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
