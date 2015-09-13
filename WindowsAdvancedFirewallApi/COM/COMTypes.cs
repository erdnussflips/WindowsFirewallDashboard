using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.COM
{
	public static class COMTypes
	{
		//public static readonly Type INetFwMgr = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
		public static readonly Type INetFwMgr = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
		public static readonly Type INetAuthApp = Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"));
		public static readonly Type INetOpenPort = Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"));
		public static readonly Type INetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2", false);
		public static readonly Type INetFwRule2 = Type.GetTypeFromProgID("HNetCfg.FwRule", false);
	}
}
