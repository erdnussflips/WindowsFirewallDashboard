using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum FirewallProfileType
	{
		Unknown,
		Domain,
		Private,
		Public
	}

	public static class FirewallProfileTypeUtil
	{
		public static IList<FirewallProfileType> ToFirewallProfileTypes(this NET_FW_PROFILE_TYPE2_ nativeType)
		{
			return ToFirewallProfileTypes((int)nativeType);
		}

		public static IList<FirewallProfileType> ToFirewallProfileTypes(this int profileTypeBitmask)
		{
			var profiles = new List<FirewallProfileType>();

			if (profileTypeBitmask == (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_ALL)
			{
				profiles.Add(FirewallProfileType.Domain);
				profiles.Add(FirewallProfileType.Private);
				profiles.Add(FirewallProfileType.Public);
				return profiles;
			}

			if ((profileTypeBitmask & (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN) == (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN)
			{
				profiles.Add(FirewallProfileType.Domain);
			}

			if ((profileTypeBitmask & (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE) == (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE)
			{
				profiles.Add(FirewallProfileType.Private);
			}

			if ((profileTypeBitmask & (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC) == (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC)
			{
				profiles.Add(FirewallProfileType.Public);
			}

			return profiles;
		}

		public static int ToNativeBitmask(this IList<FirewallProfileType> profiles)
		{
			var bitmask = 0;

			if (profiles.Contains(FirewallProfileType.Domain))
			{
				bitmask |= (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN;
			}

			if (profiles.Contains(FirewallProfileType.Private))
			{
				bitmask |= (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE;
			}

			if (profiles.Contains(FirewallProfileType.Public))
			{
				bitmask |= (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC;
			}

			return bitmask;
		}
	}
}
