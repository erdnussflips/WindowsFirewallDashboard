using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsAdvancedFirewallApi.Events.Objects;

namespace WindowsAdvancedFirewallApi.Events
{
	public class FirewallEventManager
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		public enum InstallationStatus
		{
			Installed, NotInstalled, Unknown
		}

		const string FIREWALL_EVENT_SOURCE = ApiConstants.FIREWALL_EVENT_SOURCE;
		const string FIREWALL_EVENT_PROTOCOL = ApiConstants.FIREWALL_EVENT_PROTOCOL;
		const string FIREWALL_EVENT_LOGNAME = ApiConstants.FIREWALL_EVENT_LOGNAME;

		const string REGISTRY_KEY_EVENTLOG = ApiConstants.REGISTRY_KEY_EVENTLOG;
		const string REGISTRY_KEY_FIREWALL_LOG = ApiConstants.REGISTRY_KEY_FIREWALL_LOG;

		private static FirewallEventManager instance;

		public static FirewallEventManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new FirewallEventManager();
				}
				return instance;
			}
		}

		public static FirewallEventManager Get => Instance;

		private EventLog _eventLog { get; set; }

		public event EventHandler<FirewallSettingEventArgs> SettingsChanged;

		public event EventHandler<FirewallRuleEventArgs> RulesChanged;
		public event EventHandler<FirewallRuleEventArgs> RuleAdded;
		public event EventHandler<FirewallRuleEventArgs> RuleModified;
		public event EventHandler<FirewallRuleEventArgs> RuleDeleted;

		private FirewallEventManager()
		{
			
		}


		public bool IsInstalled()
		{
			ApiHelper.RaiseExceptionOnUnauthorizedAccess("to check installation status.", true);

			using (var key = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY_FIREWALL_LOG))
			{
				if (key == null)
				{
					return false;
				}

				return true;
			}
		}

		public InstallationStatus GetInstallationStatus()
		{
			try
			{
				if(IsInstalled())
				{
					return InstallationStatus.Installed;
				}

				return InstallationStatus.NotInstalled;
			}
			catch (UnauthorizedAccessException ex)
			{
				return InstallationStatus.Unknown;
			}
		}

		//[PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
		public void Install()
		{
			ApiHelper.RaiseExceptionOnUnauthorizedAccess("to install the event logger.", true);

			if(!IsInstalled())
			{
				Registry.LocalMachine.CreateSubKey(REGISTRY_KEY_FIREWALL_LOG);
			}

			//if (!EventLog.SourceExists(FIREWALL_EVENT_SOURCE))
			//{
			//	EventLog.CreateEventSource(FIREWALL_EVENT_SOURCE, FIREWALL_EVENT_LOGNAME);
			//}
		}

		public void Deinstall()
		{
			ApiHelper.RaiseExceptionOnUnauthorizedAccess("to deinstall the event logger.", true);

			if(IsInstalled())
			{
				Registry.LocalMachine.DeleteSubKey(REGISTRY_KEY_FIREWALL_LOG);
			}

			//if(EventLog.SourceExists(FIREWALL_EVENT_SOURCE))
			//{
			//	EventLog.DeleteEventSource(FIREWALL_EVENT_SOURCE);
			//	EventLog.Delete(FIREWALL_EVENT_LOGNAME);
			//}
		}

		public bool CanListenFirewall()
		{
			try
			{
				var temp = new EventLog(FIREWALL_EVENT_LOGNAME)
				{
					EnableRaisingEvents = true
				};
				temp.Close();
				temp.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool StartListingFirewall()
		{
			if(CanListenFirewall())
			{
				if (_eventLog == null)
				{
					_eventLog = new EventLog(FIREWALL_EVENT_LOGNAME)
					{
						EnableRaisingEvents = true,
					};
					_eventLog.EntryWritten += EventLog_EntryWritten;
				}

				return true;
			}

			return false;
		}

		public bool StopListingFirewall()
		{
			if(_eventLog != null)
			{
				_eventLog.Close();
				_eventLog.Dispose();

				return true;
			}

			return false;
		}

		private void EventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
		{
			var eventId = e?.Entry?.InstanceId;

			switch ((ApiConstants.EventID)eventId)
			{
				case ApiConstants.EventID.FIREWALL_SETTING_GENERAL:
				case ApiConstants.EventID.FIREWALL_SETTING_PROFILE:
					RaiseFirewallSettingEvent(e);
					break;
				case ApiConstants.EventID.FIREWALL_RULE_ADDED:
				case ApiConstants.EventID.FIREWALL_RULE_MODIFIED:
				case ApiConstants.EventID.FIREWALL_RULE_DELETED:
					RaiseFirewallRuleEvent(e);
					break;
				default:
					LOG.Info(string.Format("The event id '{0}' is not handled by this API", eventId));
					break;
			}
		}

		private void RaiseFirewallSettingEvent(EntryWrittenEventArgs e)
		{
			var @event = new FirewallSettingEventArgs(e);
			SettingsChanged?.Invoke(this, @event);
		}

		private void RaiseFirewallRuleEvent(EntryWrittenEventArgs e)
		{
			var @event = new FirewallRuleEventArgs(e);
			RulesChanged?.Invoke(this, @event);
		}

		~FirewallEventManager()
		{
			StopListingFirewall();
		}
	}
}
