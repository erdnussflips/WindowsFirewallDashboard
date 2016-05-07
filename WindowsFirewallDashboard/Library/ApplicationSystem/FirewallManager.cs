using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsFirewallCore;
using WindowsFirewallCore.Extensions;
using WindowsFirewallCore.IPCommunication.Interfaces;
using WindowsFirewallDashboard.Library.Interfaces;
using WindowsFirewallDashboard.Library.ShellIntegration;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class FirewallManager
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		public FirewallEventManager EventManager
		{
			get
			{
				return FirewallEventManager.Instance;
			}
		}

		private readonly INotificationManager _notifications;
		private ServiceHost _shellService;

		public FirewallProfile CurrentProfile => FirewallCOMManager.Instance.CurrentProfile;

		public FirewallProfile PrivateProfile => FirewallProfile.Private;
		public FirewallProfile PublicProfile => FirewallProfile.Public;
		public FirewallProfile DomainProfile => FirewallProfile.Domain;

		public ObservableCollection<IFirewallRule> Rules { get; set; }

		public FirewallManager(INotificationManager notifications)
		{
			_notifications = notifications;
			Rules = new ObservableCollection<IFirewallRule>(FirewallCOMManager.Instance.Rules);
		}

		public void StartEventListening()
		{
			EventManager.StartListingFirewall();
			EventManager.SettingsChanged += EventManager_SettingsChanged;
			EventManager.ProfileSettingsChanged += EventManager_ProfileSettingsChanged;
			EventManager.RulesChanged += EventManager_RulesChanged;
		}

		public void StopEventListening()
		{
			EventManager.StopListingFirewall();
		}

		private void EventManager_RulesChanged(object sender, FirewallRuleBaseEventArgs e)
		{
			LOG.Debug(nameof(EventManager_RulesChanged));
			//Rules.Clear();
			// var rules = FirewallCOMManager.Instance.Rules;
			var added = FirewallCOMManager.Instance.RulesAdded;

			Debugger.Break();
		}

		private void EventManager_SettingsChanged(object sender, FirewallSettingEventArgs e)
		{
			LOG.Debug(nameof(EventManager_SettingsChanged));
			_notifications.ShowEventNotification();
		}

		private void EventManager_ProfileSettingsChanged(object sender, FirewallProfileSettingEventArgs e)
		{
			LOG.Debug(nameof(EventManager_ProfileSettingsChanged));
			_notifications.ShowEventNotification();
		}

		public void StartIPCommunication()
		{
			LOG.Debug("Open shell service");
			_shellService = new ServiceHost(new ShellServiceListener());
			_shellService.AddServiceEndpoint(typeof(IShellService), new NetNamedPipeBinding(), CoreConstants.PipeEndpoint);
			_shellService.Open();
			LOG.Debug("Shell service opened");
		}

		public void StopIPCommunication()
		{
			LOG.Debug("Close shell service");
			_shellService.Close();
			LOG.Debug("Shell service closed");
		}
	}
}
