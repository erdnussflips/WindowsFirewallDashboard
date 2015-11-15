using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallRule : FirewallObject
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ApplicationPath { get; set; }
		public string ServiceName { get; set; }
		public int Direction { get; set; }
		public int Protocol { get; set; }
		public string LocalPorts { get; set; }
		public string RemotePorts { get; set; }
		public string Action { get; set; }
		public string LocalAddresses { get; set; }
		public string RemoteAddresses { get; set; }
		public string RemoteMachineAuthorizationList { get; set; }
		public string RemoteUserAuthorizationList { get; set; }
		public string EmbeddedContext { get; set; }
		public int Flags { get; set; }
		public bool Active { get; set; }
		public int EdgeTraversal { get; set; }
		public int LooseSourceMapped { get; set; }
		public int SecurityOptions { get; set; }
		public int SchemaVersion { get; set; }
		public long Status { get; set; }
		public int LocalOnlyMapped { get; set; }
	}
}
