using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallPolicy
{
	public class OutboundPolicyParameter : PolicyParameter<OutboundPolicyParameter>
	{
		public static readonly OutboundPolicyParameter BlockOutbound = new OutboundPolicyParameter { Value = "blockoutbound" };
		public static readonly OutboundPolicyParameter AllowOutbound = new OutboundPolicyParameter { Value = "allowoutbound" };

		public static readonly OutboundPolicyParameter Default = AllowOutbound;
	}
}
