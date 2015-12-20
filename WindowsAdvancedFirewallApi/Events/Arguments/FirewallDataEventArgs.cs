using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events.Objects;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public abstract class FirewallDataEventArgs<TData> : FirewallBaseEventArgs
		where TData : FirewallBaseObject, new()
	{
		protected TData Data { get; set; }

		internal FirewallDataEventArgs(EventLogEntry @event) : base (@event)
		{
			Data = new TData();
		}
	}
}
