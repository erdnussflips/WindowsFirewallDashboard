using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace WindowsFirewallCore.FirewallController
{
	public enum FirewallStatus
	{
		Enabled, PartialEnabled, Disabled
	}
	public abstract class AController
	{
		[DllImport("shell32.dll", EntryPoint = "IsUserAnAdmin")]
		public static extern bool HasAdministratorPrivileges();

		protected static Object getFwObject(String typeName)
		{
			Type type;

			switch (typeName)
			{
				case "INetFwMgr":
					{
						//type = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
						type = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
						break;
					}
				case "INetAuthApp":
					{
						type = Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"));
						break;
					}
				case "INetOpenPort":
					{
						type = Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"));
						break;
					}
				case "INetFwPolicy2":
					{
						type = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
						break;
					}
				case "INetFwRule2":
					{
						type = Type.GetTypeFromProgID("HNetCfg.FwRule");
						break;
					}
				default:
					return null;
			}

			return Activator.CreateInstance(type);
		}

		public abstract FirewallStatus GetFirewallStatus();

		public abstract bool EnableFirewall();

		public abstract bool DisableFirewall();
	}
}
