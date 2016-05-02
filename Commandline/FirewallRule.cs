using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Enum.Firewall;
using WindowsAdvancedFirewallApi.Commandline.Enum.RuleParameter;

namespace WindowsAdvancedFirewallApi.Commandline
{
	public class FirewallRule
	{
		public string Name;
		public RuleAttributes.Direction Direction;
		public RuleAttributes.Action Action;
		public string Program;
		public object Service;
		public string Description;
		public RuleAttributes.Activatable Enable;
		public List<FirewallProfile> Profile;
		public object LocalIpAdresses;
		public object RemoteIpAdresses;
		public object LocalPorts;
		public object RemotePorts;
		public object Protocol;
		public RuleAttributes.InterfaceType InterfaceType;
		public string RemoteComputerGroup;
		public string RemoteUserGroup;
		public RuleAttributes.Edge Edge;
		public RuleAttributes.Security Security;
    }
}
