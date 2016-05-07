using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.WindowsRegistry.Types;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using System.Diagnostics;

namespace WindowsAdvancedFirewallApi.WindowsRegistry.Factories
{
	class FirewallRuleFactory
	{
		public IFirewallRule CreateFromString(string ruleId, string ruleString)
		{
			var rule = new FirewallRule
			{
				Id = ruleId
			};

			Parse(ruleString, rule);

			return rule;
		}

		private void Parse(string ruleString, FirewallRule rule)
		{
			var propertyParts = ruleString.Split('|');

			Debugger.Break();
		}
	}
}
