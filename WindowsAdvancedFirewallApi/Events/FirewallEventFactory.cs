using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events.Arguments;

namespace WindowsAdvancedFirewallApi.Events
{
	class FirewallEventFactory
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		public static FirewallBaseEventArgs GenerateFirewallEventArgs(EventLogEntry e)
		{
			var eventId = e?.InstanceId;

			switch ((WFEvents)eventId)
			{
				case WFEvents.WFGlobalConfigurationChangedEvent:
					return new FirewallSettingEventArgs(e);
				case WFEvents.WFProfileConfigurationChangedEvent:
					return new FirewallProfileSettingEventArgs(e);
				case WFEvents.WFRuleAddEvent:
				case WFEvents.WFRuleChangeEvent:
					return new FirewallRuleAddedEventArgs(e);
				case WFEvents.WFRuleDeleteEvent:
					return new FirewallRuleDeletedEventArgs(e);
				case WFEvents.WFInterfaceProfileChangedEvent:
					return new FirewallNetworkInterfaceProfileChangedEventArgs(e);
				case WFEvents.WFUnableToShowQueryUserNotificationEvent:
					return new FirewallUserNotificationFailedEventArgs(e);
				case WFEvents.WFRestoreDefaultsEvent:
				case WFEvents.WFAllFirewallRulesDeletedEvent:
				default:
					LOG.Info(string.Format("The event id '{0}' is not handled by this API.", eventId));
					return null;
			}
		}
	}
}
