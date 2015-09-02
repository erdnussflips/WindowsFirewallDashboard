using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public abstract class RuleMultipleValueParameter<ParameterType, ParameterValueType> : NetshExtendedParameter<ParameterType, ParameterValueType>
		where ParameterType : RuleParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new()
	{
		internal RuleMultipleValueParameter(string name, ParameterValueType value) : base(name, value) { }
	}
}
