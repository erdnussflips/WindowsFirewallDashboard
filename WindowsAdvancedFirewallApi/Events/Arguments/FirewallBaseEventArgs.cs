using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events.Objects;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public abstract class FirewallBaseEventArgs : EventArgs
	{
		protected static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		protected EventLogEntry FirewallLogEvent { get; set; }
		public long LogId { get; protected set; }
		public long EventId { get; protected set; }
		public DateTime ModifyingTime { get; protected set; }

		internal FirewallBaseEventArgs(EventLogEntry eventArgs)
		{
			LOG.Debug("Create new instance of " + this.GetType().Name);

			if (eventArgs == null)
			{
				var exception = new ArgumentOutOfRangeException("The given parameter " + nameof(eventArgs) + " is null.");
				LOG.Error(exception, "Wrong parameter");
				throw exception;
			}

			FirewallLogEvent = eventArgs;
			LogId = FirewallLogEvent.Index;
			EventId = FirewallLogEvent.InstanceId;
			ModifyingTime = FirewallLogEvent.TimeGenerated;
		}
	}

	public abstract class FirewallBaseEventArgs<TData> : FirewallBaseEventArgs
		where TData : FirewallBaseObject, new()
	{
		protected TData Data { get; set; }

		internal FirewallBaseEventArgs(EventLogEntry @event) : base(@event)
		{
			Data = new TData();
		}
	}
}
