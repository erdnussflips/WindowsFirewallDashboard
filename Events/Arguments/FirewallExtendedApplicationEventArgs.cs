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
	public abstract class FirewallExtendedApplicationEventArgs<TData> : FirewallEventArgs<TData> where TData : FirewallBaseObject, new()
	{
		public string ModifyingApplication { get; protected set; }

		internal FirewallExtendedApplicationEventArgs(EventLogEntry @event) : base(@event)
		{
		}

		protected void SetAttributes(int iModifiyingUser, int iModifyingApplication)
		{
			SetAttributes(iModifiyingUser);

			ModifyingApplication = FirewallLogEvent.ReplacementStrings[iModifyingApplication];
		}
	}
}
