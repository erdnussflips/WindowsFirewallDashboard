using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Objects;
using WindowsAdvancedFirewallApi.Commandline.Parameter;
using WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule;

namespace WindowsAdvancedFirewallApi.Commandline.Commands
{
	public class FirewallCommands : NetshCommands
	{
		public static NetshResult AddRule(FirewallRule rule)
		{
			var checkResult = rule.checkParameter();

			if(checkResult.Count > 0)
			{
				throw new ArgumentException(
					nameof(rule),
					"The rule is not correct specified. Use " + nameof(rule.checkParameter)
					+ "to check this first. More information at " + NetshConstants.TECHNET_NETSH_DOCU.AbsolutePath);
			}

			return RunCommand("add rule", rule.ToString(), "firewall");
		}
	}
}
