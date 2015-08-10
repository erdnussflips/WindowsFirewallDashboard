using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Value
{
	public class NetshConfigurableValue<ParameterValueType> : NetshExtendedParameterValue<ParameterValueType>
		where ParameterValueType : NetshConfigurableValue<ParameterValueType>, new()
	{
		public static ParameterValueType NotConfigured = new ParameterValueType { NetshValue = NetshConstants.NOT_CONFIGURED };
	}
}
