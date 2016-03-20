using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.FirewallEvent
{
	[Serializable]
	public abstract class ARule : ARuleGeneral
	{
		// EventID: 2004, 2005

		public int Origin;

		public string ApplicationPath;

		public string ServiceName;

		public int Direction;

		public int Protocol;

		public string LocalPorts;

		public string RemotePorts;

		public int Action;

		public int Profiles;

		public string LocalAddresses;

		public string RemoteAddresses;

		public string RemoteMachineAuthorizationList;

		public string RemoteUserAuthorizationList;

		public string EmbeddedContext;

		public int Flags;

		public int Active;

		public int EdgeTraversal;

		public int LooseSourceMapped;

		public int SecurityOptions;

		public int SchemaVersion;

		public int RuleStatus;

		public int LocalOnlyMapped;
	}
}
