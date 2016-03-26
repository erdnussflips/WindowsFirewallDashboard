using NetFwTypeLib;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data.Interfaces;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	internal class FirewallPolicy : COMWrapperType<INetFwPolicy2>
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		private FirewallProfile _currentProfile;
		public FirewallProfile CurrrentProfile
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

		private bool SetFirewallStatus(FirewallProfile profile, bool status)
		{
			try
			{
				COMObject.FirewallEnabled[profile.COMObject] = status;
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}

		public bool Enable(FirewallProfile profile)
		{
			return SetFirewallStatus(profile, true);
		}

		public bool Disable(FirewallProfile profile)
		{
			return SetFirewallStatus(profile, false);
		}

		public FirewallProfile.Status GetProfileStatus(FirewallProfile profile)
		{
			return COMObject.FirewallEnabled[profile.COMObject] ? FirewallProfile.Status.Enabled : FirewallProfile.Status.Disabled;
		}

		public void SetProfileStatus(FirewallProfile profile, FirewallProfile.Status status)
		{
			COMObject.FirewallEnabled[profile.COMObject] = (status == FirewallProfile.Status.Enabled);
		}


		[STAThread]
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
	}
}
