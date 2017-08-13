using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Library;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallRule : FirewallBaseObject, IFirewallRule
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ApplicationPath { get; set; }
		public string ServiceName { get; set; }
		public FirewallDirection Direction { get; set; }
		public FirewallProtocol Protocol { get; set; }
		public FirewallPorts LocalPorts { get; set; }
		public FirewallPorts RemotePorts { get; set; }
		public FirewallAction Action { get; set; }
		public IComparableList<FirewallProfileType> Profiles { get; set; }
		public FirewallAddresses LocalAddresses { get; set; }
		public FirewallAddresses RemoteAddresses { get; set; }
		public string RemoteMachineAuthorizationList { get; set; }
		public string RemoteUserAuthorizationList { get; set; }
		public string EmbeddedContext { get; set; }
		public int Flags { get; set; }
		public bool Active { get; set; }
		public FirewallEdgeTraversal EdgeTraversal { get; set; }
		public int LooseSourceMapped { get; set; }
		public int SecurityOptions { get; set; }
		public int SchemaVersion { get; set; }
		public long Status { get; set; }
		public int LocalOnlyMapped { get; set; }


		public string LocalAppPackageId { get; set; }
		public string LocalUserAuthorizedList { get; set; }
		public string LocalUserOwner { get; set; }
		public string Description { get; set; }
		public string ApplicationName { get; set; }
		public bool Enabled { get; set; }
		public string Grouping { get; set; }
		public string IcmpTypesAndCodes { get; set; }
		public dynamic Interfaces { get; set; }
		public string InterfaceTypes { get; set; }
	}
}
