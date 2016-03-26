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

		#region Convert functions
		private static RuleAction Convert(NET_FW_ACTION_ _action)
		{
			switch (_action)
			{
				case NET_FW_ACTION_.NET_FW_ACTION_BLOCK:
					return RuleAction.Block;
				case NET_FW_ACTION_.NET_FW_ACTION_ALLOW:
					return RuleAction.Allow;
				case NET_FW_ACTION_.NET_FW_ACTION_MAX:
					return RuleAction.Max;
				default:
					return RuleAction.Unknown;
			}
		}

		private static NET_FW_ACTION_ Convert(RuleAction _action)
		{
			switch (_action)
			{
				case RuleAction.Block:
					return NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
				case RuleAction.Allow:
					return NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
				case RuleAction.Max:
				default:
					return NET_FW_ACTION_.NET_FW_ACTION_MAX;
			}
		}

		private static RuleDirection Convert(NET_FW_RULE_DIRECTION_ _direction)
		{
			switch (_direction)
			{
				case NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN:
					return RuleDirection.In;
				case NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT:
					return RuleDirection.Out;
				case NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX:
					return RuleDirection.Max;
				default:
					return RuleDirection.Unkown;
			}
		}

		private static NET_FW_RULE_DIRECTION_ Convert(RuleDirection _direction)
		{
			switch (_direction)
			{
				case RuleDirection.In:
					return NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
				case RuleDirection.Out:
					return NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
				case RuleDirection.Max:
				default:
					return NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX;
			}
		}
		#endregion

		public RuleAction Action
		{
			get
			{
				return Convert(COMObject.Action);
			}

			set
			{
				COMObject.Action = Convert(value);
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

		public RuleDirection Direction
		{
			get
			{
				return Convert(COMObject.Direction);
			}

			set
			{
				COMObject.Direction = Convert(value);
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

		public int Protocol
		{
			get
			{
				return COMObject.Protocol;
			}

			set
			{
				COMObject.Protocol = value;
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
