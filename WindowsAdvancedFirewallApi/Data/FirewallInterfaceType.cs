using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum FirewallInterfaceType
	{
		Unknown, All, LAN, Wireless, RemoteAccess
	}

	internal static class FirewallInterfaceTypeUtil
	{
		private static readonly Dictionary<FirewallInterfaceType, string> _mappingStrings = new Dictionary<FirewallInterfaceType, string>
		{
			{ FirewallInterfaceType.All, "ALL" },
			{ FirewallInterfaceType.LAN, "LAN" },
			{ FirewallInterfaceType.Wireless, "Wireless" },
			{ FirewallInterfaceType.RemoteAccess, "RemoteAccess" },
		};

		public static FirewallInterfaceType ToFirewallInterfaceType(this string _value)
		{
			return _mappingStrings.GetKey(_value, FirewallInterfaceType.Unknown);
		}

		public static string ToNativeEnum(this FirewallInterfaceType _value)
		{
			return _mappingStrings.GetValue(_value, null);
		}
	}
}
