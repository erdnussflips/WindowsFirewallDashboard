using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;

namespace WindowsAdvancedFirewallApi.COM
{
	public sealed class Firewall
	{
		public enum Status
		{
			Enabled, PartiallyEnabled, Disabled
		}

		private static void checkForAdminRights()
		{
			if(!ApiHelper.HasAdministratorPrivileges())
			{
				throw new UnauthorizedAccessException("You need administrator rights for this function.");
			}
		}

		public static FirewallProfile CurrentProfile()
		{
			checkForAdminRights();
			return FirewallPolicy.Instance.CurrrentProfile;
		}

		public static void RestoreDefaults()
		{
			checkForAdminRights();
			FirewallPolicy.Instance.RestoreDefaults();
		}

		public static Status CurrentStatus()
		{
			checkForAdminRights();
			var states = new List<FirewallProfile.Status>
			{
				FirewallProfile.Public.CurrentStatus,
				FirewallProfile.Domain.CurrentStatus,
				FirewallProfile.Private.CurrentStatus
			};

			var enabledProfiles = states.Contains(FirewallProfile.Status.Enabled);
			var disabledProfiles = states.Contains(FirewallProfile.Status.Disabled);

			if (enabledProfiles && !disabledProfiles) return Status.Enabled;
			if (!enabledProfiles && disabledProfiles) return Status.Disabled;

			return Status.PartiallyEnabled;
		}

		public static bool Enable(FirewallProfile profile)
		{
			checkForAdminRights();
			if (profile == FirewallProfile.All) throw new ArgumentException("The 'All' profile can't used for enabling");
			return FirewallPolicy.Instance.Enable(profile);
		}

		public static bool Disable(FirewallProfile profile)
		{
			checkForAdminRights();
			if (profile == FirewallProfile.All) throw new ArgumentException("The 'All' profile can't used for disabling");
			return FirewallPolicy.Instance.Disable(profile);
		}
	}
}
