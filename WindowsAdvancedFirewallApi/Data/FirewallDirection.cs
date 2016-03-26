using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum FirewallDirection
	{
		Unkown, In, Out, Max
	}

	internal static class  FirewallDirectionUtil
	{
		public static FirewallDirection ToManagedEnum(this NET_FW_RULE_DIRECTION_ _direction)
		{
			switch (_direction)
			{
				case NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN:
					return FirewallDirection.In;
				case NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT:
					return FirewallDirection.Out;
				case NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX:
					return FirewallDirection.Max;
				default:
					return FirewallDirection.Unkown;
			}
		}

		public static NET_FW_RULE_DIRECTION_ ToNativeEnum(this FirewallDirection _direction)
		{
			switch (_direction)
			{
				case FirewallDirection.In:
					return NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
				case FirewallDirection.Out:
					return NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
				case FirewallDirection.Max:
				default:
					return NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX;
			}
		}

		public static FirewallDirection ToFirewallDirection(this int _eventDirection)
		{
			switch (_eventDirection)
			{
				case 1:
					return FirewallDirection.In;
				case 2:
					return FirewallDirection.Out;
				default:
					return FirewallDirection.Unkown;
			}
		}
	}
}
