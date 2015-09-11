using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public abstract class RuleSingleParameter<ParameterType, ParameterValueType> : NetshExtendedSingleValueParameter<ParameterType, ParameterValueType>
		where ParameterType : RuleSingleParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new()
	{
		internal RuleSingleParameter(string name, ParameterValueType value) : base(name, value) { }
	}
}
