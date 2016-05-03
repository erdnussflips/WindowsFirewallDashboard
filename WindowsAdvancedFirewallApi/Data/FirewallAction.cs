using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum FirewallAction
	{
		Unknown, Block, Allow, Max
	}

	internal static class FirewallActionUtil
	{
		private static readonly Dictionary<FirewallAction, NET_FW_ACTION_> _mappingNatives = new Dictionary<FirewallAction, NET_FW_ACTION_>
		{
			{ FirewallAction.Block, NET_FW_ACTION_.NET_FW_ACTION_BLOCK },
			{ FirewallAction.Allow, NET_FW_ACTION_.NET_FW_ACTION_ALLOW },
			{ FirewallAction.Max, NET_FW_ACTION_.NET_FW_ACTION_MAX },
		};

		private static readonly Dictionary<FirewallAction, int> _mappingEvents = new Dictionary<FirewallAction, int>
		{
			{ FirewallAction.Allow, 2 },
			{ FirewallAction.Block, 3 },
		};

		public static FirewallAction ToManagedEnum(this NET_FW_ACTION_ _value)
		{
			return _mappingNatives.GetKey(_value, FirewallAction.Unknown);
		}

		public static NET_FW_ACTION_ ToNativeEnum(this FirewallAction _value)
		{
			return _mappingNatives.GetValue(_value, NET_FW_ACTION_.NET_FW_ACTION_BLOCK);
		}

		public static FirewallAction ToFirewallAction(this int _eventAction)
		{
			return _mappingEvents.GetKey(_eventAction, FirewallAction.Unknown);
		}
	}
}
