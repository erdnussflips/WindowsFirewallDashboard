using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class FirewallProfile : COMWrapperType<NET_FW_PROFILE_TYPE2_>
	{
		public enum Status
		{
			Enabled, Disabled
		}

		public static FirewallProfile All = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_ALL);
		public static FirewallProfile Private = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE);
		public static FirewallProfile Domain = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);
		public static FirewallProfile Public = new FirewallProfile(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC);

		public Status CurrentStatus
		{
			get
			{
				return FirewallPolicy.Instance.GetProfileStatus(this);
			}
			set
			{
				FirewallPolicy.Instance.SetProfileStatus(this, value);
			}
		}

		internal FirewallProfile(NET_FW_PROFILE_TYPE2_ type) : base()
		{
			COMObject = type;
		}

		internal FirewallProfile(int type) : this((NET_FW_PROFILE_TYPE2_)type) { }
	}
}
