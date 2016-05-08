using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum FirewallDirection
	{
		Unknown, In, Out, Max
	}

	internal static class FirewallDirectionUtil
	{
		private static readonly Dictionary<FirewallDirection, NET_FW_RULE_DIRECTION_> _mappingNatives = new Dictionary<FirewallDirection, NET_FW_RULE_DIRECTION_>
		{
			{ FirewallDirection.In, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN },
			{ FirewallDirection.Out, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT },
			{ FirewallDirection.Max, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX },
		};

		private static readonly Dictionary<FirewallDirection, int> _mappingEvents = new Dictionary<FirewallDirection, int>
		{
			{ FirewallDirection.In, 1 },
			{ FirewallDirection.Out, 2 },
		};

		private static readonly Dictionary<FirewallDirection, string> _mappingRegistry = new Dictionary<FirewallDirection, string>
		{
			{ FirewallDirection.In, "In" },
			{ FirewallDirection.Out, "Out" },
		};

		public static FirewallDirection ToManagedEnum(this NET_FW_RULE_DIRECTION_ _value)
		{
			return _mappingNatives.GetKey(_value, FirewallDirection.Unknown);
		}

		public static NET_FW_RULE_DIRECTION_ ToNativeEnum(this FirewallDirection _value)
		{
			return _mappingNatives.GetValue(_value, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX);
		}

		public static FirewallDirection ToFirewallDirection(this int _eventAction)
		{
			return _mappingEvents.GetKey(_eventAction, FirewallDirection.Unknown);
		}

		public static FirewallDirection ToFirewallDirection(this string _registryAction)
		{
			return _mappingRegistry.GetKey(_registryAction, FirewallDirection.Unknown);
		}
	}
}
