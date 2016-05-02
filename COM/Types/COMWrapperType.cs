using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class COMWrapperType<TManagedCOMType>
	{
		protected sealed class Native<TManagedNativeType>
		{
			public Type NativeType { get; private set; }

			private Native(Type comType)
			{
				this.NativeType = comType;
			}

			public static readonly Native<INetFwMgr> INetFwMgr = new Native<INetFwMgr>(Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}")));
			public static readonly Native<INetFwAuthorizedApplication> INetAuthApp = new Native<INetFwAuthorizedApplication>(Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}")));
			public static readonly Native<INetFwOpenPort> INetOpenPort = new Native<INetFwOpenPort>(Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}")));
			public static readonly Native<INetFwPolicy2> INetFwPolicy2 = new Native<INetFwPolicy2>(Type.GetTypeFromProgID("HNetCfg.FwPolicy2", false));
			public static readonly Native<INetFwRule> INetFwRule = new Native<INetFwRule>(Type.GetTypeFromProgID("HNetCfg.FwRule", false));
			public static readonly Native<INetFwRule2> INetFwRule2 = new Native<INetFwRule2>(Type.GetTypeFromProgID("HNetCfg.FwRule", false));
			public static readonly Native<INetFwRule3> INetFwRule3 = new Native<INetFwRule3>(Type.GetTypeFromProgID("HNetCfg.FwRule", false));
		}

		protected internal TManagedCOMType COMObject { get; protected set; }

		protected COMWrapperType()
		{

		}

		protected COMWrapperType(TManagedCOMType comObject)
		{
			COMObject = comObject;
		}

		protected COMWrapperType(Native<TManagedCOMType> type)
		{
			COMObject = (TManagedCOMType)Activator.CreateInstance(type.NativeType);
		}

		private COMWrapperType(Type type)
		{
			COMObject = (TManagedCOMType)Activator.CreateInstance(type);
		}

		private COMWrapperType(string progID)
		{
			COMObject = (TManagedCOMType)Activator.CreateInstance(Type.GetTypeFromProgID(progID));
		}
	}
}
