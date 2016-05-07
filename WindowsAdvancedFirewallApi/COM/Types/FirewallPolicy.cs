using NetFwTypeLib;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	internal static class INetFwRulesExtension
	{
		public static IEnumerable<INetFwRule> ToTypedEnumerable(this INetFwRules rules)
		{
			var typedRules = new List<INetFwRule>();

			foreach (var rule in rules)
			{
				if (rule is INetFwRule)
				{
					typedRules.Add(rule as INetFwRule);
				}
			}

			return typedRules;
		}
	}


	internal class FirewallPolicy : COMWrapperType<INetFwPolicy2>
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		private FirewallProfile _currentProfile;
		public FirewallProfile CurrentProfile
		{
			get
			{
				if(_currentProfile == null)
				{
					_currentProfile = new FirewallProfile(COMObject.CurrentProfileTypes);
				}

				return _currentProfile;
			}
		}

		internal FirewallPolicy() : base(Native<INetFwPolicy2>.INetFwPolicy2) { }

		public void RestoreDefaults()
		{
			COMObject.RestoreLocalFirewallDefaults();
		}

		private bool SetFirewallStatus(FirewallProfile profile, Status status)
		{
			try
			{
				COMObject.FirewallEnabled[profile.COMObject] = status.ToBoolean();
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public bool Enable(FirewallProfile profile)
		{
			return SetFirewallStatus(profile, Status.Enabled);
		}

		public bool Disable(FirewallProfile profile)
		{
			return SetFirewallStatus(profile, Status.Disabled);
		}

		public Status GetProfileStatus(FirewallProfile profile)
		{
			return COMObject.FirewallEnabled[profile.COMObject].ToStatusEnum();
		}

		public void SetProfileStatus(FirewallProfile profile, Status status)
		{
			COMObject.FirewallEnabled[profile.COMObject] = status.ToBoolean();
		}

		public IList<IFirewallRule> GetRules()
		{
			LOG.Debug("List rules");
			var rules = new List<IFirewallRule>();

			foreach (var item in COMObject.Rules)
			{
				if (!(item is INetFwRule3)) continue;

				var nativeRule = item as INetFwRule3;
				var managedRule = new FirewallRule(nativeRule);
				rules.Add(managedRule);
			}

			return rules;
		}

		public IDictionary<INetFwRule, IFirewallRule> GetRuleDictionary()
		{
			return GetRuleDictionary(COMObject.Rules);
		}

		public IDictionary<INetFwRule, IFirewallRule> GetRuleAddedDictionary(IEnumerable<INetFwRule> oldNativeRules)
		{
			var nativeRules = COMObject.Rules.ToTypedEnumerable();

			var added = new HashSet<INetFwRule>(nativeRules);
			added.ExceptWith(oldNativeRules);

			return GetRuleDictionary(added);
		}

		public IDictionary<INetFwRule, IFirewallRule> GetRuleDeletedDictionary(IEnumerable<INetFwRule> oldNativeRules)
		{
			var nativeRules = COMObject.Rules.ToTypedEnumerable();

			var added = GetRuleAddedDictionary(oldNativeRules).Keys.ToList();

			var oldAndAddedRules = new HashSet<INetFwRule>(oldNativeRules);
			oldAndAddedRules.AddRange(added);

			var deleted = new HashSet<INetFwRule>(oldAndAddedRules);
			deleted.SymmetricExceptWith(nativeRules);

			return GetRuleDictionary(deleted);
		}

		private IDictionary<INetFwRule, IFirewallRule> GetRuleDictionary(IEnumerable nativeRules)
		{
			LOG.Debug("List rules");
			var rules = new Dictionary<INetFwRule, IFirewallRule>();

			foreach (var item in nativeRules)
			{
				if (!(item is INetFwRule3)) continue;

				var nativeRule = item as INetFwRule3;
				var managedRule = new FirewallRule(nativeRule);
				rules.Add(nativeRule, managedRule);
			}

			return rules;
		}

		public IFirewallRule GetRule()
		{
			return null;
		}

		public void SetBlockAllInboundTraffic(FirewallProfile profile, Status status)
		{
			COMObject.BlockAllInboundTraffic[profile.COMObject] = status.ToBoolean();
		}

		public Status GetBlockAllInboundTraffic(FirewallProfile profile)
		{
			return COMObject.BlockAllInboundTraffic[profile.COMObject].ToStatusEnum();
		}

		public void SetDefaultInboundAction(FirewallProfile profile, FirewallAction action)
		{
			COMObject.DefaultInboundAction[profile.COMObject] = action.ToNativeEnum();
		}

		public FirewallAction GetDefaultInboundAction(FirewallProfile profile)
		{
			return COMObject.DefaultInboundAction[profile.COMObject].ToManagedEnum();
		}

		public void SetDefaultOutboundAction(FirewallProfile profile, FirewallAction action)
		{
			COMObject.DefaultOutboundAction[profile.COMObject] = action.ToNativeEnum();
		}

		public FirewallAction GetDefaultOutboundAction(FirewallProfile profile)
		{
			return COMObject.DefaultOutboundAction[profile.COMObject].ToManagedEnum();
		}

		public bool IsNotificationDisabled(FirewallProfile profile)
		{
			return COMObject.NotificationsDisabled[profile.COMObject];
		}

		public void SetNotificationDisabled(FirewallProfile profile, bool value)
		{
			COMObject.NotificationsDisabled[profile.COMObject] = value;
		}
	}
}
