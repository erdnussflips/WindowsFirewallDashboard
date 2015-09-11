using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule.Value
{
	public class PortValue<ParameterValueType> : NetshAnyValue<ParameterValueType>
		where ParameterValueType : PortValue<ParameterValueType>, new()
	{
		private static void checkPortnumber(int port)
		{
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException(nameof(port), "The given port must between 0 and 65535.");
			}
		}

		public static ParameterValueType Custom(int port)
		{
			checkPortnumber(port);
			return new ParameterValueType { Value = port.ToString() };
		}

		public static ParameterValueType Custom(int portBegin, int portEnd)
		{
			checkPortnumber(portBegin); checkPortnumber(portEnd);
			return new ParameterValueType { Value = portBegin.ToString() + "-" + portEnd.ToString() };
		}
	}
}
