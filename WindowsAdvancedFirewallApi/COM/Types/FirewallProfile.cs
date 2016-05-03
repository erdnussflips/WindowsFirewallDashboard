using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class FirewallProfile : COMWrapperType<NET_FW_PROFILE_TYPE2_>
	{
		public static readonly FirewallProfile All = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_ALL);
		public static readonly FirewallProfile Private = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE);
		public static readonly FirewallProfile Domain = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);
		public static readonly FirewallProfile Public = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC);

		public Status CurrentStatus
		{
			get
			{
				return FirewallCOMManager.Instance.Policy.GetProfileStatus(this);
			}
			set
			{
				FirewallCOMManager.Instance.Policy.SetProfileStatus(this, value);
			}
		}

		public bool IsEnabled
		{
			get
			{
				return CurrentStatus.ToBoolean();
			}
			set
			{
				CurrentStatus = value.ToStatusEnum();
			}
		}

		public FirewallAction DefaultInboundAction
		{
			get
			{
				return FirewallCOMManager.Instance.Policy.GetDefaultInboundAction(this);
			}
			set
			{
				FirewallCOMManager.Instance.Policy.SetDefaultInboundAction(this, value);
			}
		}

		public FirewallAction DefaultOutboundAction
		{
			get
			{
				return FirewallCOMManager.Instance.Policy.GetDefaultOutboundAction(this);
			}
			set
			{
				FirewallCOMManager.Instance.Policy.SetDefaultOutboundAction(this, value);
			}
		}

		public FirewallProfileType ManagedType
		{
			get
			{
				var profileTypes = COMObject.ToFirewallProfileTypes();

				if (profileTypes?.Count == 1)
				{
					return profileTypes[0];
				}

				return FirewallProfileType.Unknown;
			}
		}

		public bool RuleNotification
		{
			get
			{
				return !FirewallCOMManager.Instance.Policy.IsNotificationDisabled(this);
			}
			set
			{
				FirewallCOMManager.Instance.Policy.SetNotificationDisabled(this, !value);
			}
		}

		internal FirewallProfile(NET_FW_PROFILE_TYPE2_ type) {
			COMObject = type;
		}

		internal FirewallProfile(int type) : this((NET_FW_PROFILE_TYPE2_)type) { }

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is FirewallProfile)) return false;
			var profile = obj as FirewallProfile;

			return COMObject == profile.COMObject;
		}

		public static bool operator == (FirewallProfile obj1, FirewallProfile obj2)
		{
			return obj1?.COMObject == obj2?.COMObject;
		}
		public static bool operator != (FirewallProfile obj1, FirewallProfile obj2)
		{
			return !(obj1 == obj2);
		}
	}
}
