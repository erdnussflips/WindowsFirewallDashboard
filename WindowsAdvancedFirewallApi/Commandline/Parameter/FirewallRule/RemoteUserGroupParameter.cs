using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public class RemoteUserGroupParameter : RuleSingleParameter<RemoteUserGroupParameter, RemoteUserGroupParameter.Value>
	{
		public class Value : NetshExtendedParameterValue<Value>
		{
			public static Value Custom(string SDDLString)
			{
				return new Value { Value = SDDLString };
			}
		}

		public RemoteUserGroupParameter(Value value) : base("rmtusrgrp", value) { }
		public RemoteUserGroupParameter() : this(null) { }
	}
}
