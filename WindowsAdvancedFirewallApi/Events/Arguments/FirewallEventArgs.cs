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
	public abstract class FirewallEventArgs<TData> : FirewallDataEventArgs<TData> where TData : FirewallBaseObject, new()
	{
		public int Origin { get; protected set; }
		public string ModifyingUser { get; protected set; }
		public string ModifyingApplication { get; protected set; }

		internal FirewallEventArgs(EventLogEntry @event) : base(@event)
		{
		}

		protected void SetAttributes(int iOrigin, int iModifiyingUser, int iModifyingApplication)
		{
			LOG.Debug("ReplacementStrings: {0}", string.Join(",", FirewallLogEvent.ReplacementStrings));

			Origin = PrimitiveUtils.ParseInteger(FirewallLogEvent.ReplacementStrings[iOrigin], 0);
			ModifyingUser = FirewallLogEvent.ReplacementStrings[iModifiyingUser];
			ModifyingApplication = FirewallLogEvent.ReplacementStrings[iModifyingApplication];
		}
	}
}
