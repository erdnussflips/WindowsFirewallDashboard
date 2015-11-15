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
	public abstract class FirewallEventArgs<T> : EventArgs
		where T : FirewallObject, new()
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		protected EntryWrittenEventArgs FirewallLogEventArgs { get; set; }

		public long EventId { get; protected set; }

		public int Origin { get; protected set; }
		public string ModifyingUser { get; protected set; }
		public string ModifyingApplication { get; protected set; }

		protected T Data { get; set; }

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

			Data = new T();
		}

		protected void SetAttributes(int iProfiles, int iOrigin, int iModifiyingUser, int iModifyingApplication)
		{
			try
			{
				Data.Profiles = (FirewallObject.Profile)int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[iProfiles]);

				Origin = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[iOrigin]);
				ModifyingUser = FirewallLogEventArgs.Entry.ReplacementStrings[iModifiyingUser];
				ModifyingApplication = FirewallLogEventArgs.Entry.ReplacementStrings[iModifyingApplication];
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info(string.Format("Primitive parse error: {0}", string.Join(",", FirewallLogEventArgs.Entry.ReplacementStrings)));
				LOG.Debug(ex);
			}
		}
	}
}
