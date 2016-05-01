using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Library;

namespace WindowsAdvancedFirewallApi.Data.Interfaces
{
	public interface IFirewallRule
	{
		string Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		string ApplicationName { get; }
		string ApplicationPath { get; set; }
		string ServiceName { get; set; }
		FirewallDirection Direction { get; set; }
		FirewallProtocol Protocol { get; set; }
		FirewallPorts LocalPorts { get; set; }
		FirewallPorts RemotePorts { get; set; }
		FirewallAction Action { get; set; }
		IComparableList<FirewallProfileType> Profiles { get; set; }
		string LocalAddresses { get; set; }
		string LocalAppPackageId { get; set; }
		string LocalUserAuthorizedList { get; set; }
		string LocalUserOwner { get; set; }
		string RemoteAddresses { get; set; }
		string RemoteMachineAuthorizationList { get; set; }
		string RemoteUserAuthorizationList { get; set; }
		string EmbeddedContext { get; set; }
		int Flags { get; set; }
		bool Active { get; set; }
		FirewallEdgeTraversal EdgeTraversal { get; set; }
		int LooseSourceMapped { get; set; }
		int SecurityOptions { get; set; }
		int SchemaVersion { get; set; }
		long Status { get; set; }
		int LocalOnlyMapped { get; set; }


		bool Enabled { get; set; }
		string Grouping { get; set; }
		string IcmpTypesAndCodes { get; set; }
		dynamic Interfaces { get; set; }
		string InterfaceTypes { get; set; }
	}
}
