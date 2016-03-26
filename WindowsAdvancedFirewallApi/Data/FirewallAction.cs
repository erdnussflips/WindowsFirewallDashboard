using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum FirewallAction
	{
		Unknown, Block, Allow, Max
	}

	internal static class FirewallActionUtil
	{
		public static FirewallAction ToManagedEnum(this NET_FW_ACTION_ _action)
		{
			switch (_action)
			{
				case NET_FW_ACTION_.NET_FW_ACTION_BLOCK:
					return FirewallAction.Block;
				case NET_FW_ACTION_.NET_FW_ACTION_ALLOW:
					return FirewallAction.Allow;
				case NET_FW_ACTION_.NET_FW_ACTION_MAX:
					return FirewallAction.Max;
				default:
					return FirewallAction.Unknown;
			}
		}

		public static NET_FW_ACTION_ ToNativeEnum(this FirewallAction _action)
		{
			switch (_action)
			{
				case FirewallAction.Block:
					return NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
				case FirewallAction.Allow:
					return NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
				case FirewallAction.Max:
				default:
					return NET_FW_ACTION_.NET_FW_ACTION_MAX;
			}
		}

		public static FirewallAction ToFirewallAction(this int _eventAction)
		{
			switch (_eventAction)
			{
				case 2:
					return FirewallAction.Allow;
				case 3:
					return FirewallAction.Block;
				default:
					return FirewallAction.Unknown;
			}
		}
	}
}
