using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Value
{
	public class NetshExtendedParameterValue<ParameterValueType>
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new()
	{
		public string Value { get; protected set; }

		protected NetshExtendedParameterValue() { }
	}
}
