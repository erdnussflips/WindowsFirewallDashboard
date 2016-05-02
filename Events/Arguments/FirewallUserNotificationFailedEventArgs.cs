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
	public class FirewallUserNotificationFailedEventArgs : FirewallEventArgs<FirewallUserNotificationEvent>
	{
		public FirewallUserNotificationEvent Notification
		{
			get { return Data; }
			protected set { Data = value; }
		}

		internal FirewallUserNotificationFailedEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}

		private void SetAttributes()
		{
			SetAttributes(6);

			Notification.ReasonCode = FirewallLogEvent.ReplacementStrings[0].ParseInteger(-1);
			Notification.ApplicationPath = FirewallLogEvent.ReplacementStrings[1];
			Notification.IpVersion = FirewallLogEvent.ReplacementStrings[2].ParseInteger(-1);
			Notification.Protocol = FirewallLogEvent.ReplacementStrings[2].ParseInteger(-1);
			Notification.Port = FirewallLogEvent.ReplacementStrings[2].ParseInteger(-1);
			Notification.ProcessId = FirewallLogEvent.ReplacementStrings[2].ParseInteger(-1);
		}
	}
}
