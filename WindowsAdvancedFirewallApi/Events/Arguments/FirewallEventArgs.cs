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
	public abstract class FirewallEventArgs<TData> : EventArgs
		where TData : FirewallObject, new()
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		protected EntryWrittenEventArgs FirewallLogEventArgs { get; set; }

		public long EventId { get; protected set; }

		public int Origin { get; protected set; }
		public string ModifyingUser { get; protected set; }
		public string ModifyingApplication { get; protected set; }

		protected TData Data { get; set; }

		internal FirewallEventArgs(EntryWrittenEventArgs eventArgs)
		{
			if (eventArgs == null)
			{
				var exception = new ArgumentOutOfRangeException("The given parameter " + nameof(eventArgs) + " is null.");
				LOG.Error(exception, "Wrong parameter");
				throw exception;
			}

			FirewallLogEventArgs = eventArgs;
			EventId = FirewallLogEventArgs.Entry.InstanceId;

			Data = new TData();
		}

		protected void SetAttributes(int iProfiles, int iOrigin, int iModifiyingUser, int iModifyingApplication)
		{
			LOG.Debug("ReplacementStrings: {0}", string.Join(",", FirewallLogEventArgs.Entry.ReplacementStrings));

			Data.Profiles = EnumUtils.ParseStringValue(FirewallLogEventArgs.Entry.ReplacementStrings[iProfiles], FirewallObject.Profile.Unkown);
			Origin = PrimitiveUtils.ParseInteger(FirewallLogEventArgs.Entry.ReplacementStrings[iOrigin], 0);
			ModifyingUser = FirewallLogEventArgs.Entry.ReplacementStrings[iModifiyingUser];
			ModifyingApplication = FirewallLogEventArgs.Entry.ReplacementStrings[iModifyingApplication];
		}
	}
}
