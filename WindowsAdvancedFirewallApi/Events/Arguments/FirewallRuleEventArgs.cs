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
	public class FirewallRuleEventArgs : FirewallEventArgs<FirewallRule>
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		public FirewallRule Rule
		{
			get { return Data; }
			protected set { Data = value; }
		}

		internal FirewallRuleEventArgs(EntryWrittenEventArgs eventArgs) : base(eventArgs)
		{
			SetAttributes();
		}

		protected void SetAttributes()
		{
			SetAttributes(10, 2, 21, 22);

			try
			{
				Rule.Id = FirewallLogEventArgs.Entry.ReplacementStrings[0];
				Rule.Name = FirewallLogEventArgs.Entry.ReplacementStrings[1];
				Rule.ApplicationPath = FirewallLogEventArgs.Entry.ReplacementStrings[3];
				Rule.ServiceName = FirewallLogEventArgs.Entry.ReplacementStrings[4];
				Rule.Direction = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[5]);
				Rule.Protocol = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[6]);
				Rule.LocalPorts = FirewallLogEventArgs.Entry.ReplacementStrings[7];
				Rule.RemotePorts = FirewallLogEventArgs.Entry.ReplacementStrings[8];
				Rule.Action = FirewallLogEventArgs.Entry.ReplacementStrings[9];
				Rule.LocalAddresses = FirewallLogEventArgs.Entry.ReplacementStrings[11];
				Rule.RemoteAddresses = FirewallLogEventArgs.Entry.ReplacementStrings[12];
				Rule.RemoteMachineAuthorizationList = FirewallLogEventArgs.Entry.ReplacementStrings[13];
				Rule.RemoteUserAuthorizationList = FirewallLogEventArgs.Entry.ReplacementStrings[14];
				Rule.EmbeddedContext = FirewallLogEventArgs.Entry.ReplacementStrings[15];
				Rule.Flags = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[16]);
				Rule.Active = Convert.ToBoolean(int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[17]));
				Rule.EdgeTraversal = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[18]);
				Rule.LooseSourceMapped = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[19]);
				Rule.SecurityOptions = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[20]);
				Rule.SchemaVersion = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[23]);
				Rule.Status = long.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[24]);
				Rule.LocalOnlyMapped = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[25]);
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info(string.Format("Primitive parse error: {0}", string.Join(",", FirewallLogEventArgs.Entry.ReplacementStrings)));
				LOG.Debug(ex);
			}
		}
	}
}
