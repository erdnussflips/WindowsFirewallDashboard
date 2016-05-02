using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public abstract class NetshExtendedParameter<ParameterType> : NetshParameter
		where ParameterType : NetshExtendedParameter<ParameterType>, new()
	{
		protected internal string Name { get; }

		internal NetshExtendedParameter(string name)
		{
			Name = name;
		}

		public string ToStringAppendEmptyChar(bool prepend = false, bool forceAppend = false)
		{
			var ret = ToString();

			if (prepend) ret = " " + ret;
			if (!prepend || forceAppend) ret = ret + " ";

			return ret;
		}
	}
}
