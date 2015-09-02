using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Firewall
{
	public class ProgramParameter : RuleParameter<ProgramParameter, ProgramParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Custom(Uri programExePath)
			{
				return new Value { Value = programExePath.AbsolutePath };
			}
		}

		public ProgramParameter(Value value) : base("program", value) { }
		public ProgramParameter() : this(null) { }
	}
}
