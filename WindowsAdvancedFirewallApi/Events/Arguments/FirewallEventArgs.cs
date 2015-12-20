using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public abstract class FirewallEventArgs : EventArgs
	{
		protected static Logger LOG = LogManager.GetCurrentClassLogger();

		protected EventLogEntry FirewallLogEvent { get; set; }

		public long EventId { get; protected set; }

		public int Origin { get; protected set; }
		public string ModifyingUser { get; protected set; }
		public string ModifyingApplication { get; protected set; }

		public DateTime ModifyingTime { get; protected set; }

		internal FirewallEventArgs(EventLogEntry eventArgs)
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

		protected void SetAttributes(int iOrigin, int iModifiyingUser, int iModifyingApplication)
		{
			LOG.Debug("ReplacementStrings: {0}", string.Join(",", FirewallLogEvent.ReplacementStrings));

			Origin = PrimitiveUtils.ParseInteger(FirewallLogEvent.ReplacementStrings[iOrigin], 0);
			ModifyingUser = FirewallLogEvent.ReplacementStrings[iModifiyingUser];
			ModifyingApplication = FirewallLogEvent.ReplacementStrings[iModifyingApplication];
		}
	}
}
