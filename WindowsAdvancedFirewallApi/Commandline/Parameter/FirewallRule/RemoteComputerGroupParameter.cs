using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class RemoteComputerGroupParameter : RuleSingleParameter<RemoteComputerGroupParameter, RemoteComputerGroupParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Custom(string SDDLString)
			{
				return new Value { Value = SDDLString };
			}
		}

		public RemoteComputerGroupParameter(Value value) : base("rmtcomputergrp", value) { }
		public RemoteComputerGroupParameter() : this(null) { }
	}
}
