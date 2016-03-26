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
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsFirewallCore;
using WindowsFirewallCore.Extensions;
using WindowsFirewallCore.IPCommunication.Interfaces;
using WindowsFirewallDashboard.Library.ShellIntegration;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class FirewallManager
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

		private ObservableCollection<IFirewallRule> _rules;
		public ObservableCollection<IFirewallRule> Rules
		{
			get
			{
				return _rules;
			}
			set
			{
				_rules = value;
			}
		}

		public FirewallManager(NotificationManager notifications)
		{
			_notifications = notifications;
			Rules = new ObservableCollection<IFirewallRule>(FirewallCOMManager.Instance.Rules);
		}

		public void StartEventListening()
		{
			EventManager.StartListingFirewall();
			EventManager.SettingsChanged += EventManager_SettingsChanged;
			EventManager.RulesChanged += EventManager_RulesChanged;
		}

		private void EventManager_RulesChanged(object sender, FirewallRuleBaseEventArgs e)
		{
			LOG.Debug("Rules changed");
			Rules.Clear();
			Rules.AddRange(FirewallCOMManager.Instance.Rules);
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
