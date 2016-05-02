using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum FirewallEdgeTraversal
	{
		Deny,
		Allow,
		DeferToApp,
		DeferToUser
	}
	internal static class FirewallEdgeTraversalUtil
	{
		private static Dictionary<FirewallEdgeTraversal, int> _mappingNatives = new Dictionary<FirewallEdgeTraversal, int>
		{
			{ FirewallEdgeTraversal.Deny, 0 },
			{ FirewallEdgeTraversal.Allow, 1 },
			{ FirewallEdgeTraversal.DeferToApp, 2 },
			{ FirewallEdgeTraversal.DeferToUser, 3 },
		};

		public static FirewallEdgeTraversal ToFirewallEdgeTraversal(this int _value)
		{
			return _mappingNatives.GetKey(_value, FirewallEdgeTraversal.Deny);
		}

		public static int ToNativeValue(this FirewallEdgeTraversal _value)
		{
			return _mappingNatives.GetValue(_value, 0);
		}
	}
}
