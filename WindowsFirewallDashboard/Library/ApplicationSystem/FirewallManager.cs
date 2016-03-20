using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsFirewallCore;
using WindowsFirewallCore.IPCommunication.ShellIntegration.Interfaces;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class FirewallManager : IShellService
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		public FirewallEventManager EventManager
		{
			get
			{
				return FirewallEventManager.Instance;
			}
		}

		private NotificationManager _notifications;
		private ServiceHost _shellService;

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

		public void StartIPCommunication()
		{
			_shellService = new ServiceHost(this);
			_shellService.AddServiceEndpoint(typeof(IShellService), new NetNamedPipeBinding(), CoreConstants.PipeEndpoint);
			_shellService.Open();
		}

		public void StopIPCommunication()
		{
			_shellService.Close();
		}

		public void GetApplicationStatus(string applicationFilePath)
		{
			LOG.Info(applicationFilePath);
		}

		public void AddApplicationRule(string applicationFilePath)
		{
			LOG.Info(applicationFilePath);
		}

		public void RemoveApplicationRule(string applicationFilePath)
		{
			LOG.Info(applicationFilePath);
		}
	}
}
