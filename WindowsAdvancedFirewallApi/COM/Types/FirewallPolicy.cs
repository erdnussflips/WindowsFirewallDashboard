using NetFwTypeLib;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Data.Interfaces;

namespace WindowsAdvancedFirewallApi.COM.Types
{
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
	}
}
