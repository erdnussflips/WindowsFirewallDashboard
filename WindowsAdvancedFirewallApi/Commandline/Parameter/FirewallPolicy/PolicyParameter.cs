using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallPolicy
{
	public class PolicyParameter<PolicyParameterType> : NetshConfigurableParameter<PolicyParameterType>
		where PolicyParameterType : NetshConfigurableParameter<PolicyParameterType>, new()
	{
	}
}
