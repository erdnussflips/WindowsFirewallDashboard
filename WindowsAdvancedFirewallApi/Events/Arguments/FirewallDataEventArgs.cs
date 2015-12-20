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
	public abstract class FirewallDataEventArgs<TData> : FirewallEventArgs
		where TData : FirewallObject, new()
	{
		protected TData Data { get; set; }

		internal FirewallDataEventArgs(EventLogEntry @event) : base (@event)
		{
			Data = new TData();
		}

		protected void SetAttributes(int iProfiles, int iOrigin, int iModifiyingUser, int iModifyingApplication)
		{
			SetAttributes(iOrigin, iModifiyingUser, iModifyingApplication);

			Data.Profiles = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[iProfiles], FirewallObject.Profile.Unkown);
		}
	}
}
