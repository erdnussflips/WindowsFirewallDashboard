using NetFwTypeLib;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

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


		private IDictionary<string, IHashedFirewallRule> RuleDictionary;
		public IList<IFirewallRule> Rules
		{
			get
			{
				checkForAdminRights();
				RuleDictionary = Policy.GetRuleDictionary();
				return RuleDictionary.Values.Cast<IFirewallRule>().ToList();
			}
		}

		public IList<IFirewallRule> RulesAdded
		{
			get
			{
				checkForAdminRights();
				var addedRules = new List<IFirewallRule>();

				var oldRules = RuleDictionary;
				var currentRules = Policy.GetRuleDictionary();

				var hashesOldRules = oldRules.Keys;
				var hashesCurrentRules = currentRules.Keys;

				var hashSetRules = new HashSet<string>(hashesCurrentRules);
				hashSetRules.ExceptWith(hashesOldRules);

				foreach (var item in hashSetRules)
				{
					var rule = currentRules.GetValue(item, null);

					if (rule != null)
					{
						addedRules.Add(rule);
						RuleDictionary.Add(rule.InitContentHashCode, rule);
					}
				}

				return addedRules;
			}
		}

		public IList<IFirewallRule> RulesRemoved
		{
			get
			{
				checkForAdminRights();
				var removedRules = new List<IFirewallRule>();

				var oldRules = RuleDictionary;
				var currentRules = Policy.GetRuleDictionary();

				var hashesOldRules = oldRules.Keys;
				var hashesCurrentRules = currentRules.Keys;

				var hashesExistingRules = new HashSet<string>(hashesCurrentRules);
				hashesExistingRules.IntersectWith(hashesOldRules);

				var hashesRemovedRules = new HashSet<string>(hashesOldRules);
				hashesRemovedRules.ExceptWith(hashesExistingRules);


				foreach (var item in hashesRemovedRules)
				{
					var rule = oldRules.GetValue(item, null);

					if (rule != null)
					{
						removedRules.Add(rule);
						RuleDictionary.Remove(rule.InitContentHashCode);
					}
				}

				return removedRules;
			}
		}

		public IList<IFirewallRule> RulesModified
		{
			get
			{
				checkForAdminRights();
				var modifiedRules = new List<IFirewallRule>();

				if (RuleDictionary == null)
				{
					return modifiedRules;
				}

				var oldRules = RuleDictionary;
				var currentRules = Policy.GetRuleDictionary();

				var hashesOldRules = oldRules.Keys;
				var hashesCurrentRules = currentRules.Keys;

				var difference = new HashSet<string>(hashesOldRules);
				difference.SymmetricExceptWith(hashesCurrentRules);

				foreach (var item in oldRules.Values)
				{
					if ((bool)item.Name?.Equals("FirewallTestRule"))
					{
						LOG.Debug($"Old:{item.InitContentHashCode}:{item.ContentHashCode}:{item.Name}:{item.Description}");
					}
				}

				foreach (var item in currentRules.Values)
				{
					if ((bool)item.Name?.Equals("FirewallTestRule"))
					{
						LOG.Debug($"Current:{item.InitContentHashCode}:{item.ContentHashCode}:{item.Name}:{item.Description}");
					}
				}

				/*foreach (var item in oldRules)
				{
					var rule = item.Value;
					var initHash = rule.InitContentHashCode;
					var description = rule.Description;

					if ((bool)rule.Name?.Equals("FirewallTestRule"))
					{
						Debugger.Break();
					}

					var isPresent = hashesCurrentRules.Contains(hash);
					if (isPresent)
					{

					}

					if (isPresent && !initHash.Equals(hash))
					{
						modifiedRules.Add(rule);
					}
				}*/

				return modifiedRules;
			}
		}
	}
}
