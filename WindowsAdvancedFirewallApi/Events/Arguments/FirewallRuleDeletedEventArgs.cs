using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallRuleDeletedEventArgs : FirewallRuleBaseEventArgs
	{
		public FirewallRuleDeletedEventArgs(EventLogEntry @event) : base(@event)
		{

		}
	}
}
