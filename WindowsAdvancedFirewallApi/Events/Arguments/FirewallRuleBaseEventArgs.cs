using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events.Objects;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public abstract class FirewallRuleBaseEventArgs : FirewallEventArgs<FirewallRule>
	{
		public FirewallRule Rule
		{
			get { return Data; }
			protected set { Data = value; }
		}

		internal FirewallRuleBaseEventArgs(EventLogEntry @event) : base(@event)
		{
		}
	}
}
