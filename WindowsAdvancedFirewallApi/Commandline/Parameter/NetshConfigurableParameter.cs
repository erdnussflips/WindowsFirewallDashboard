using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public class NetshConfigurableParameter<ParameterType> : NetshSimpleParameter
		where ParameterType : NetshConfigurableParameter<ParameterType>, new()
	{
		public static ParameterType NotConfigured = new ParameterType { Value = NetshConstants.NOT_CONFIGURED };
	}
}
