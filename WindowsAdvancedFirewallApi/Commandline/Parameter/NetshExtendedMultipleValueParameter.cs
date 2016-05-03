using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public abstract class NetshExtendedMultipleValueParameter<ParameterType, ParameterValueType> : NetshExtendedParameter<ParameterType>
		where ParameterType : NetshExtendedMultipleValueParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new()
	{
		private List<ParameterValueType> _parameterValues;
		public List<ParameterValueType> ParameterValues
		{
			get
			{
				if(_parameterValues == null)
				{
					_parameterValues = new List<ParameterValueType>();
				}

				return _parameterValues;
			}

			internal set
			{
                _parameterValues = value;
            }
		}

		internal NetshExtendedMultipleValueParameter(string name, params ParameterValueType[] values) : base(name)
		{
			if(values != null)
			{
				ParameterValues.AddRange(values);
			}
		}

		public override string ToString()
		{
			var strBuilder = new StringBuilder();
			strBuilder.Append(Name + "=");
			var iterator = ParameterValues.GetEnumerator();

			if(ParameterValues.Count > 1)
			{
				do
				{
					strBuilder.Append(iterator.Current + ",");
				} while (iterator.MoveNext());
			}

			strBuilder.Append(iterator.Current);

			return strBuilder.ToString();
		}

		public void AddValue(ParameterValueType value)
		{
			ParameterValues.Add(value);
		}

		public void RemoveValue(ParameterValueType value)
		{
			ParameterValues.Remove(value);
		}
	}
}
