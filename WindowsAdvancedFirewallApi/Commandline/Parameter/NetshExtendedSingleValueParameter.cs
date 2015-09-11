using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public abstract class NetshExtendedSingleValueParameter<ParameterType, ParameterValueType> : NetshExtendedParameter<ParameterType>
		where ParameterType : NetshExtendedSingleValueParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new ()
	{
		public ParameterValueType ParameterValue;

		internal NetshExtendedSingleValueParameter(string name) : base(name) { }

		internal NetshExtendedSingleValueParameter(string name, ParameterValueType value) : this(name)
		{
			if(value != null)
			{
				ParameterValue = value;
			}
		}

		public override string ToString()
		{
			return Name + "=" + ParameterValue.Value;
		}
	}
}