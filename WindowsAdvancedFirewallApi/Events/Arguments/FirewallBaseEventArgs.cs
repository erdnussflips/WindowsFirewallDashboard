using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public abstract class FirewallBaseEventArgs : EventArgs
	{
		protected static Logger LOG = LogManager.GetCurrentClassLogger();

		protected EventLogEntry FirewallLogEvent { get; set; }
		public long EventId { get; protected set; }
		public DateTime ModifyingTime { get; protected set; }

		internal FirewallBaseEventArgs(EventLogEntry eventArgs)
		{
			if (eventArgs == null)
			{
				var exception = new ArgumentOutOfRangeException("The given parameter " + nameof(eventArgs) + " is null.");
				LOG.Error(exception, "Wrong parameter");
				throw exception;
			}

			FirewallLogEvent = eventArgs;
			EventId = FirewallLogEvent.InstanceId;
			ModifyingTime = FirewallLogEvent.TimeGenerated;
		}
	}
}
