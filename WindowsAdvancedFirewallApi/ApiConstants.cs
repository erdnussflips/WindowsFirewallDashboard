using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events;

namespace WindowsAdvancedFirewallApi
{
	internal static class ApiConstants
	{
		public const string FIREWALL_EVENT_SOURCE = "Windows Firewall With Advanced Security";
		public const string FIREWALL_EVENT_PROTOCOL = "Microsoft-Windows-Windows Firewall With Advanced Security/Firewall";
		public const string FIREWALL_EVENT_LOGNAME = FIREWALL_EVENT_PROTOCOL;

		public const string REGISTRY_KEY_EVENTLOG = @"SYSTEM\CurrentControlSet\Services\EventLog\";
		public const string REGISTRY_KEY_FIREWALL_LOG = REGISTRY_KEY_EVENTLOG + FIREWALL_EVENT_PROTOCOL;

		public const string REGISTRY_KEY_RULES =				@"SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\FirewallRules\";
		public const string REGISTRY_KEY_RULES_STATIC =			@"SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\RestrictedServices\Static\System\";
		public const string REGISTRY_KEY_RULES_CONFIGURABLE =	@"SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\RestrictedServices\Configurable\System\";
	}
}
