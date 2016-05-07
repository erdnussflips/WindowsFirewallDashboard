using NetFwTypeLib;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Library;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class FirewallRule : COMWrapperType<INetFwRule3>, IFirewallRule, IHashedContent
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		public string InitContentHashCode { get; private set; }

		public string ContentHashCode
		{
			get
			{
				return CalculateContentHashCode();
			}
		}

		private string CalculateContentHashCode()
		{
			var contentStrings = new StringBuilder();

			foreach (var property in typeof(IFirewallRule).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				var value = property.GetValue(this);

				if (value is ICollection)
				{
					var castedObject = value as ICollection;

					contentStrings.Append($"{castedObject.Stringify()};");
				}
				else
				{
					contentStrings.Append($"{value?.ToString()};");
				}
			}

			return HashTool.MD5(contentStrings.ToString());
		}

		public FirewallRule() : base(Native<INetFwRule3>.INetFwRule3)
		{

		}

		internal FirewallRule(INetFwRule3 nativeObject) : base(nativeObject)
		{
			InitContentHashCode = CalculateContentHashCode();
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
				return COMObject.ApplicationName.GetFileNameOfPath();
			}
		}

		public string ApplicationPath
		{
			get
			{
				var applicationName = COMObject.ApplicationName;
				if (!applicationName.IsPath())
				{
					return null;
				}

				return applicationName;
			}

			set
			{
				COMObject.ApplicationName = value;
			}
		}

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

		public FirewallEdgeTraversal EdgeTraversal
		{
			get
			{
				return COMObject.EdgeTraversalOptions.ToFirewallEdgeTraversal();
			}

			set
			{
				COMObject.EdgeTraversalOptions = value.ToNativeValue();
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

		public IComparableList<FirewallProfileType> Profiles
		{
			get
			{
				return new ComparableList<FirewallProfileType>(COMObject.Profiles.ToFirewallProfileTypes());
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

		public FirewallAddresses LocalAddresses
		{
			get
			{
				return COMObject.LocalAddresses.ToFirewallAddresses();
			}

			set
			{
				COMObject.LocalAddresses = value.ToNativeValue();
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

		public FirewallPorts LocalPorts
		{
			get
			{
				return COMObject.LocalPorts?.ToFirewallPorts();
			}

			set
			{
				COMObject.LocalPorts = value?.ToNativeValue();
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

		public FirewallPorts RemotePorts
		{
			get
			{
				return COMObject.RemotePorts.ToFirewallPorts();
			}

			set
			{
				COMObject.RemotePorts = value.ToNativeValue();
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
