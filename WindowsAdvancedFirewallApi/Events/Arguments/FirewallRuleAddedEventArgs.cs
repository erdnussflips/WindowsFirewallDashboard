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
	public class FirewallRuleAddedEventArgs : FirewallRuleBaseEventArgs
	{
		public FirewallRuleAddedEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}
		protected void SetAttributes()
		{
			SetAttributes(2, 21, 22);

			try
			{
				Rule.Id = FirewallLogEvent.ReplacementStrings[0];
				Rule.Name = FirewallLogEvent.ReplacementStrings[1];
				Rule.ApplicationPath = FirewallLogEvent.ReplacementStrings[3];
				Rule.ServiceName = FirewallLogEvent.ReplacementStrings[4];
				Rule.Direction = int.Parse(FirewallLogEvent.ReplacementStrings[5]);
				Rule.Protocol = int.Parse(FirewallLogEvent.ReplacementStrings[6]);
				Rule.LocalPorts = FirewallLogEvent.ReplacementStrings[7];
				Rule.RemotePorts = FirewallLogEvent.ReplacementStrings[8];
				Rule.Action = FirewallLogEvent.ReplacementStrings[9];
				Rule.Profiles = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[10], FirewallBaseObject.Profile.Unkown);
				Rule.LocalAddresses = FirewallLogEvent.ReplacementStrings[11];
				Rule.RemoteAddresses = FirewallLogEvent.ReplacementStrings[12];
				Rule.RemoteMachineAuthorizationList = FirewallLogEvent.ReplacementStrings[13];
				Rule.RemoteUserAuthorizationList = FirewallLogEvent.ReplacementStrings[14];
				Rule.EmbeddedContext = FirewallLogEvent.ReplacementStrings[15];
				Rule.Flags = int.Parse(FirewallLogEvent.ReplacementStrings[16]);
				Rule.Active = Convert.ToBoolean(int.Parse(FirewallLogEvent.ReplacementStrings[17]));
				Rule.EdgeTraversal = int.Parse(FirewallLogEvent.ReplacementStrings[18]);
				Rule.LooseSourceMapped = int.Parse(FirewallLogEvent.ReplacementStrings[19]);
				Rule.SecurityOptions = int.Parse(FirewallLogEvent.ReplacementStrings[20]);
				Rule.SchemaVersion = int.Parse(FirewallLogEvent.ReplacementStrings[23]);
				Rule.Status = long.Parse(FirewallLogEvent.ReplacementStrings[24]);
				Rule.LocalOnlyMapped = int.Parse(FirewallLogEvent.ReplacementStrings[25]);
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info(string.Format("Primitive parse error: {0}", string.Join(",", FirewallLogEvent.ReplacementStrings)));
				LOG.Debug(ex);
			}
		}
	}
}
