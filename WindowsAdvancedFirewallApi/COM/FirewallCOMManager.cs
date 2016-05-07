using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.COM
{
	public sealed class FirewallCOMManager
	{
		public enum FirewallStatus
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

		private static void validateProfile(FirewallProfile profile)
		{
			if (profile == FirewallProfile.All)
				throw new ArgumentException("The '"+nameof(FirewallProfile.All)+"' profile can't used for this action.");
		}

		private static void validateForNativeProfileAccess(FirewallProfile profile)
		{
			checkForAdminRights();
			validateProfile(profile);
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
				return Policy.CurrentProfile;
			}
		}

		public void RestoreDefaults()
		{
			checkForAdminRights();
			Policy.RestoreDefaults();
		}

		public FirewallStatus CurrentStatus
		{
			get
			{
				checkForAdminRights();
				var states = new List<Status>
				{
					FirewallProfile.Public.CurrentStatus,
					FirewallProfile.Domain.CurrentStatus,
					FirewallProfile.Private.CurrentStatus
				};

				var enabledProfiles = states.Contains(Status.Enabled);
				var disabledProfiles = states.Contains(Status.Disabled);

				if (enabledProfiles && !disabledProfiles) return FirewallStatus.Enabled;
				if (!enabledProfiles && disabledProfiles) return FirewallStatus.Disabled;

				return FirewallStatus.PartiallyEnabled;
			}
		}

		public bool Enable(FirewallProfile profile)
		{
			validateForNativeProfileAccess(profile);
			return Policy.Enable(profile);
		}

		public bool Disable(FirewallProfile profile)
		{
			validateForNativeProfileAccess(profile);
			return Policy.Disable(profile);
		}

		public void EnableNotifications(FirewallProfile profile)
		{
			validateForNativeProfileAccess(profile);
			Policy.SetNotificationDisabled(profile, false);
		}

		public void DisableNotifications(FirewallProfile profile)
		{
			validateForNativeProfileAccess(profile);
			Policy.SetNotificationDisabled(profile, true);
		}


		private IDictionary<INetFwRule, IFirewallRule> RuleDictionary;
		public IList<IFirewallRule> Rules
		{
			get
			{
				checkForAdminRights();
				RuleDictionary = Policy.GetRuleDictionary();
				return RuleDictionary.Values.ToList();
			}
		}

		public IList<IFirewallRule> RulesAdded
		{
			get
			{
				checkForAdminRights();
				var addedRules = new Dictionary<INetFwRule, IFirewallRule>();

				if (RuleDictionary == null)
				{
					return addedRules.Values.ToList();
				}

				addedRules.AddRange(Policy.GetRuleAddedDictionary(RuleDictionary.Keys.ToList()));

				return addedRules.Values.ToList();
			}
		}
	}
}
