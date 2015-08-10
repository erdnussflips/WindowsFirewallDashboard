using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Value
{
	public class NetshControllableValue<ParameterValueType> : NetshConfigurableValue<ParameterValueType>
		where ParameterValueType : NetshControllableValue<ParameterValueType>, new()
	{
		public static ParameterValueType Enable = new ParameterValueType { NetshValue = "enable" };
		public static ParameterValueType Disable = new ParameterValueType { NetshValue = "disable" };
	}
}
