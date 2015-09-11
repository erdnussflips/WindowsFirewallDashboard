using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	internal class FirewallPolicy : COMWrapperType<INetFwPolicy2>
	{
		private static FirewallPolicy _instance;
		public static FirewallPolicy Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = new FirewallPolicy();
				}

				return _instance;
			}
		}

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

		private FirewallPolicy() : base(COMTypes.INetFwPolicy2) { }

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
			COMObject.FirewallEnabled[profile.COMObject] = (status == FirewallProfile.Status.Enabled) ? true : false;
		}
	}
}
