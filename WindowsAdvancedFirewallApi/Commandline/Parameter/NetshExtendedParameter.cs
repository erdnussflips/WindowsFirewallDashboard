using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public abstract class NetshExtendedParameter<ParameterType, ParameterValueType> : NetshParameter
		where ParameterType : NetshExtendedParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new ()
    {
		internal string Name { get; set; }
		public ParameterValueType value;
	}
}
