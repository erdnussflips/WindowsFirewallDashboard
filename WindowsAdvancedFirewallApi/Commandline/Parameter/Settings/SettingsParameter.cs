using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Settings
{
	public class SettingsParameter<ParameterType, ParameterValueType> : NetshExtendedParameter<ParameterType, ParameterValueType>
		where ParameterType : SettingsParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new()
	{
		internal SettingsParameter(string name, ParameterValueType value) : base(name, value) { }
	}
}
