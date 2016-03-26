using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Events.Objects;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallNetworkInterfaceProfileChangedEventArgs : FirewallBaseEventArgs<FirewallNetworkInterfaceProfile>
	{
		public FirewallNetworkInterfaceProfile NetworkInterface
		{
			get { return Data; }
			protected set { Data = value; }
		}

		public FirewallNetworkInterfaceProfileChangedEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}

		protected void SetAttributes()
		{
			NetworkInterface.InterfaceGUID = FirewallLogEvent.ReplacementStrings[0];
			NetworkInterface.InterfaceName = FirewallLogEvent.ReplacementStrings[1];
			NetworkInterface.OldProfile = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[2], FirewallProfileType.Unknown);
			NetworkInterface.NewProfile = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[3], FirewallProfileType.Unknown);
		}
	}
}
