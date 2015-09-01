using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Logging
{
	public abstract class LoggingParameter<ParameterType, ParameterValueType> : NetshExtendedParameter<ParameterType, ParameterValueType>
		where ParameterType : LoggingParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new()
	{
		internal LoggingParameter(string name, ParameterValueType value) : base(name, value) { }

	}
}
