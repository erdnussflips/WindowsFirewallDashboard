﻿using NetFwTypeLib;
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
				return FirewallCOMManager.Instance.Policy.GetProfileStatus(this);
			}
			set
			{
				FirewallCOMManager.Instance.Policy.SetProfileStatus(this, value);
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
			return obj1.COMObject == obj2.COMObject;
		}
		public static bool operator != (FirewallProfile obj1, FirewallProfile obj2)
		{
			return obj1.COMObject != obj2.COMObject;
		}
	}
}
