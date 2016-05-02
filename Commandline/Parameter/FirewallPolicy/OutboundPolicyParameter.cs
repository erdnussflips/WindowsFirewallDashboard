using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallPolicy
{
	public class OutboundPolicyParameter : PolicyParameter<OutboundPolicyParameter>
	{
		public static OutboundPolicyParameter BlockOutbound = new OutboundPolicyParameter { Value = "blockoutbound" };
		public static OutboundPolicyParameter AllowOutbound = new OutboundPolicyParameter { Value = "allowoutbound" };

		public static OutboundPolicyParameter Default = AllowOutbound;
	}
}
