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
	public abstract class FirewallExtendedOriginEventArgs<TData> : FirewallExtendedApplicationEventArgs<TData> where TData : FirewallBaseObject, new()
	{
		public int Origin { get; protected set; }

		internal FirewallExtendedOriginEventArgs(EventLogEntry @event) : base(@event)
		{
		}

		protected void SetAttributes(int iOrigin, int iModifiyingUser, int iModifyingApplication)
		{
			SetAttributes(iModifiyingUser, iModifyingApplication);

			Origin = PrimitiveUtils.ParseInteger(FirewallLogEvent.ReplacementStrings[iOrigin], 0);
		}
	}
}
