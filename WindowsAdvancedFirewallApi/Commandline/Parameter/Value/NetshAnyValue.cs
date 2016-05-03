using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Value
{
	public class NetshAnyValue<ParameterValueType> : NetshExtendedParameterValue<ParameterValueType>
		where ParameterValueType : NetshAnyValue<ParameterValueType>, new()
	{
		public static readonly ParameterValueType Any = new ParameterValueType { Value = "any" };
	}
}
