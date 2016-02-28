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

		private static FirewallEventManager _singleton;
		private static FirewallEventManager Singleton
		{
			get
			{
				if (_singleton == null)
				{
					_singleton = new FirewallEventManager();
				}
				return _singleton;
			}
		}

		public static FirewallEventManager Instance => Singleton;
		public static FirewallEventManager Get => Singleton;

		private EventLog _eventLog { get; set; }

		#region EventHander

		public event EventHandler<FirewallHistoryLoadingStatusChangedEventArgs> HistoryLoadingStatusChanged;
		public event EventHandler<List<FirewallBaseEventArgs>> HistoryLoaded;

		public event EventHandler<FirewallBaseEventArgs> DefaultsRestored;
		public event EventHandler<FirewallSettingEventArgs> SettingsChanged;
		public event EventHandler<FirewallProfileSettingEventArgs> ProfileSettingsChanged;

		public event EventHandler<FirewallRuleBaseEventArgs> AllRulesDeleted;
		public event EventHandler<FirewallRuleBaseEventArgs> RulesChanged;
		public event EventHandler<FirewallRuleAddedEventArgs> RuleAdded;
		public event EventHandler<FirewallRuleBaseEventArgs> RuleModified;
		public event EventHandler<FirewallRuleDeletedEventArgs> RuleDeleted;

		public event EventHandler<FirewallNetworkInterfaceProfileChangedEventArgs> NetworkInterfaceProfileChanged;

		public event EventHandler<FirewallUserNotificationFailedEventArgs> UserNotificationFailedForBlockedOperation;

		#endregion

		private FirewallEventManager()
		{
		}


		public bool IsInstalled()
		{
			ApiHelper.RaiseExceptionOnUnauthorizedAccess("to check installation status.", true);

			using (var key = Registry.LocalMachine.OpenSubKey(ApiConstants.REGISTRY_KEY_FIREWALL_LOG))
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
				Registry.LocalMachine.CreateSubKey(ApiConstants.REGISTRY_KEY_FIREWALL_LOG);
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
				Registry.LocalMachine.DeleteSubKey(ApiConstants.REGISTRY_KEY_FIREWALL_LOG);
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
				var temp = new EventLog(ApiConstants.FIREWALL_EVENT_LOGNAME)
				{
					EnableRaisingEvents = true
				};
				temp.Close();
				temp.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				LOG.Debug(ex.Message);
				return false;
			}
		}

		public bool StartListingFirewall()
		{
			if(CanListenFirewall())
			{
				if (_eventLog == null)
				{
					_eventLog = new EventLog(ApiConstants.FIREWALL_EVENT_LOGNAME)
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
			var @event = FirewallEventFactory.GenerateFirewallEventArgs(e.Entry);
			RaiseFirewallEvent(@event);
		}

		private void RaiseFirewallEvent(FirewallBaseEventArgs e)
		{
			InvokeEventHandler(DefaultsRestored, e);
			InvokeEventHandler(SettingsChanged, e);
			InvokeEventHandler(ProfileSettingsChanged, e);

			InvokeEventHandler(AllRulesDeleted, e);
			InvokeEventHandler(RulesChanged, e);
			InvokeEventHandler(RuleAdded, e);
			InvokeEventHandler(RuleModified, e);
			InvokeEventHandler(RuleDeleted, e);

			InvokeEventHandler(NetworkInterfaceProfileChanged, e);

			InvokeEventHandler(UserNotificationFailedForBlockedOperation, e);
		}

		private void InvokeEventHandler<TData>(EventHandler<TData> handler, EventArgs e) where TData : EventArgs
		{
			if (e is TData)
			{
				handler?.Invoke(this, (TData)e);
			}
		}

		public void LoadEventHistory()
		{
			Task.Run(() =>
			{
				var events = new List<FirewallBaseEventArgs>();
				var index = 1;

				foreach (EventLogEntry item in _eventLog.Entries)
				{
					var @event = FirewallEventFactory.GenerateFirewallEventArgs(item);
					if (@event != null)
					{
						events.Add(@event);

						var loadedEvent = new FirewallHistoryLoadingStatusChangedEventArgs()
						{
							MaxCount = _eventLog.Entries.Count,
							LoadedCount = index,
							CurrentLoadedEvent = @event
						};
						
						InvokeEventHandler(HistoryLoadingStatusChanged, loadedEvent);
					}

					index++;
				}

				HistoryLoaded?.Invoke(this, events);
			});
		}

		~FirewallEventManager()
		{
			StopListingFirewall();
		}
	}
}
