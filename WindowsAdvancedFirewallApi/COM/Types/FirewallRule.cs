using NetFwTypeLib;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class FirewallRule : COMWrapperType<INetFwRule3>, IFirewallRule
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		public FirewallRule() : base(Native<INetFwRule3>.INetFwRule3)
		{

		}

		internal FirewallRule(INetFwRule3 nativeObject) : base(nativeObject)
		{

		}

		public FirewallAction Action
		{
			get
			{
				return COMObject.Action.ToManagedEnum();
			}

			set
			{
				COMObject.Action = value.ToNativeEnum();
			}
		}

		public bool Active
		{
			get
			{
				return COMObject.Enabled;
			}
			set
			{
				COMObject.Enabled = value;
			}
		}

		public string ApplicationName
		{
			get
			{
				return COMObject.ApplicationName;
			}
			set
			{
				COMObject.ApplicationName = value;
			}
		}

		public string ApplicationPath { get; set; }

		public string Description
		{
			get
			{
				return COMObject.Description;
			}

			set
			{
				COMObject.Description = value;
			}
		}

		public FirewallDirection Direction
		{
			get
			{
				return COMObject.Direction.ToManagedEnum();
			}

			set
			{
				COMObject.Direction = value.ToNativeEnum();
			}
		}

		public int EdgeTraversal
		{
			get
			{
				return COMObject.EdgeTraversal.ConvertToInteger();
			}

			set
			{
				COMObject.EdgeTraversal = value.ConvertToBool();
			}
		}

		public string EmbeddedContext { get; set; }

		public bool Enabled
		{
			get
			{
				return Active;
			}

			set
			{
				Active = value;
			}
		}

		public int Flags { get; set; }

		public string Grouping
		{
			get
			{
				return COMObject.Grouping;
			}

			set
			{
				COMObject.Grouping = value;
			}
		}

		public string IcmpTypesAndCodes
		{
			get
			{
				return COMObject.IcmpTypesAndCodes;
			}
			set
			{
				COMObject.IcmpTypesAndCodes = value;
			}
		}

		public string Id { get; set; }

		public IList<FirewallProfileType> Profiles
		{
			get
			{
				return COMObject.Profiles.ToFirewallProfileTypes();
			}
			set
			{
				COMObject.Profiles = value.ToNativeBitmask();
			}
		}

		public dynamic Interfaces
		{
			get
			{
				return COMObject.Interfaces;
			}

			set
			{
				COMObject.Interfaces = value;
			}
		}

		public string InterfaceTypes
		{
			get
			{
				return COMObject.InterfaceTypes;
			}

			set
			{
				COMObject.InterfaceTypes = value;
			}
		}

		public string LocalAddresses
		{
			get
			{
				return COMObject.LocalAddresses;
			}

			set
			{
				COMObject.LocalAddresses = value;
			}
		}

		public string LocalAppPackageId
		{
			get
			{
				return COMObject.LocalAppPackageId;
			}
			set
			{
				COMObject.LocalAppPackageId = value;
			}
		}

		public string LocalUserAuthorizedList
		{
			get
			{
				return COMObject.LocalUserAuthorizedList;
			}
			set
			{
				COMObject.LocalUserAuthorizedList = value;
			}
		}

		public string LocalUserOwner
		{
			get
			{
				return COMObject.LocalUserOwner;
			}
			set
			{
				COMObject.LocalUserOwner = value;
			}
		}

		public int LocalOnlyMapped { get; set; }

		public string LocalPorts
		{
			get
			{
				return COMObject.LocalPorts;
			}

			set
			{
				COMObject.LocalPorts = value;
			}
		}

		public int LooseSourceMapped { get; set; }

		public string Name
		{
			get
			{
				return COMObject.Name;
			}

			set
			{
				COMObject.Name = value;
			}
		}

		public FirewallProtocol Protocol
		{
			get
			{
				return COMObject.Protocol.ToFirewallProtocol();
			}

			set
			{
				COMObject.Protocol = value.Value;
			}
		}

		public string RemoteAddresses
		{
			get
			{
				return COMObject.RemoteAddresses;
			}

			set
			{
				COMObject.RemoteAddresses = value;
			}
		}

		public string RemoteMachineAuthorizationList
		{
			get
			{
				return COMObject.RemoteMachineAuthorizedList;
			}
			set
			{
				COMObject.RemoteMachineAuthorizedList = value;
			}
		}

		public string RemotePorts
		{
			get
			{
				return COMObject.RemotePorts;
			}

			set
			{
				COMObject.RemotePorts = value;
			}
		}

		public string RemoteUserAuthorizationList
		{
			get
			{
				return COMObject.RemoteUserAuthorizedList;
			}
			set
			{
				COMObject.RemoteUserAuthorizedList = value;
			}
		}

		public int SchemaVersion { get; set; }

		public int SecurityOptions { get; set; }

		public string ServiceName
		{
			get
			{
				return COMObject.serviceName;
			}

			set
			{
				COMObject.serviceName = value;
			}
		}

		public long Status { get; set; }
	}
}
