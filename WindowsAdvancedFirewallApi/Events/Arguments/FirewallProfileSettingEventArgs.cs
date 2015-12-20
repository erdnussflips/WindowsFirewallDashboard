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
	public class FirewallProfileSettingEventArgs : FirewallSettingEventArgs
	{
		internal FirewallProfileSettingEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}

		protected new void SetAttributes()
		{
			SetAttributes(5, 6, 7);
			SetSettingAttributes(1, 2, 3, 4);

			Setting.Profiles = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[0], FirewallBaseObject.Profile.Unkown);
		}
	}
}