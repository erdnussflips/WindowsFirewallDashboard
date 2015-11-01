using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi
{
	internal class ApiConstants
	{
		public const string FIREWALL_EVENT_SOURCE = "Windows Firewall With Advanced Security";
		public const string FIREWALL_EVENT_PROTOCOL = "Microsoft-Windows-Windows Firewall With Advanced Security/Firewall";
		public const string FIREWALL_EVENT_LOGNAME = FIREWALL_EVENT_PROTOCOL;

		public const string REGISTRY_KEY_EVENTLOG = @"SYSTEM\CurrentControlSet\Services\EventLog\";
		public const string REGISTRY_KEY_FIREWALL_LOG = REGISTRY_KEY_EVENTLOG + FIREWALL_EVENT_PROTOCOL;

		internal class EventID
		{
			public const int FIREWALL_SETTING_GENERAL = 2002;
			public const int FIREWALL_SETTING_PROFILE = 2003;
			public const int FIREWALL_RULE_ADDED = 2004;
			public const int FIREWALL_RULE_MODIFIED = 2005;
			public const int FIREWALL_RULE_DELETED = 2006;
		}
	}
}
