using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events;

namespace WindowsAdvancedFirewallApi
{
	internal class ApiConstants
	{
		public const string FIREWALL_EVENT_SOURCE = "Windows Firewall With Advanced Security";
		public const string FIREWALL_EVENT_PROTOCOL = "Microsoft-Windows-Windows Firewall With Advanced Security/Firewall";
		public const string FIREWALL_EVENT_LOGNAME = FIREWALL_EVENT_PROTOCOL;

		public const string REGISTRY_KEY_EVENTLOG = @"SYSTEM\CurrentControlSet\Services\EventLog\";
		public const string REGISTRY_KEY_FIREWALL_LOG = REGISTRY_KEY_EVENTLOG + FIREWALL_EVENT_PROTOCOL;

		internal enum EventID
		{
			FIREWALL_SETTING_GENERAL = WFEvents.WFGlobalConfigurationChangedEvent,
			FIREWALL_SETTING_PROFILE = WFEvents.WFProfileConfigurationChangedEvent,

			FIREWALL_RULE_ADDED = WFEvents.WFRuleAddEvent,
			FIREWALL_RULE_MODIFIED = WFEvents.WFRuleChangeEvent,
			FIREWALL_RULE_DELETED = WFEvents.WFRuleDeleteEvent,

			FIREWALL_NETWORKINTERFACE_CHANGED = WFEvents.WFInterfaceProfileChangedEvent
		}
	}
}
