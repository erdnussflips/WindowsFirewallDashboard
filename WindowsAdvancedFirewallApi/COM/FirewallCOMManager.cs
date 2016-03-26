using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Data.Interfaces;

namespace WindowsAdvancedFirewallApi.COM
{
	public sealed class FirewallCOMManager
	{
		public enum Status
		{
			Enabled, PartiallyEnabled, Disabled
		}

		#region Statics
		private static void checkForAdminRights()
		{
			if (!ApiHelper.HasAdministratorPrivileges())
			{
				throw new UnauthorizedAccessException("You need administrator rights for this function.");
			}
		}
		#endregion

		#region Singleton
		private static FirewallCOMManager _singleton;
		public static FirewallCOMManager Singleton
		{
			get
			{
				if (_singleton == null)
				{
					_singleton = new FirewallCOMManager();
				}
				return _singleton;
			}
		}
		public static FirewallCOMManager Instance => Singleton;
		#endregion

		internal FirewallPolicy Policy { get; private set; }

		private FirewallCOMManager()
		{
			Policy = new FirewallPolicy();
		}

		public FirewallProfile CurrentProfile
		{
			get
			{
				checkForAdminRights();
				return Policy.CurrrentProfile;
			}
		}

		public void RestoreDefaults()
		{
			checkForAdminRights();
			Policy.RestoreDefaults();
		}

		public Status CurrentStatus
		{
			get
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
		}

		public bool Enable(FirewallProfile profile)
		{
			checkForAdminRights();
			if (profile == FirewallProfile.All) throw new ArgumentException("The 'All' profile can't used for enabling");
			return Policy.Enable(profile);
		}

		public bool Disable(FirewallProfile profile)
		{
			checkForAdminRights();
			if (profile == FirewallProfile.All) throw new ArgumentException("The 'All' profile can't used for disabling");
			return Policy.Disable(profile);
		}

		public IList<IFirewallRule> Rules => Policy.GetRules();
	}
}
