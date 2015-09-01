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
		protected internal string Name { get; }
		public ParameterValueType ParameterValue;

		internal NetshExtendedParameter(string name)
		{
			Name = name;
		}

		internal NetshExtendedParameter(string name, ParameterValueType value) : this(name)
		{
			if(value != null)
			{
				ParameterValue = value;
			}
		}
	}
}
