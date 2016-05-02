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
	public abstract class FirewallEventArgs<TData> : FirewallBaseEventArgs<TData> where TData : FirewallBaseObject, new()
	{
		public string ModifyingUser { get; protected set; }

		internal FirewallEventArgs(EventLogEntry @event) : base(@event)
		{
		}

		protected void SetAttributes(int iModifiyingUser)
		{
			LOG.Debug("ReplacementStrings: {0}", string.Join(",", FirewallLogEvent.ReplacementStrings));

			ModifyingUser = FirewallLogEvent.ReplacementStrings[iModifiyingUser];
		}
	}
}
