using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallHistoryLoadingStatusChangedEventArgs
	{
		public int LoadedCount { get; internal set; }
		public int MaxCount { get; internal set; }
		public FirewallBaseEventArgs CurrentLoadedEvent { get; internal set; }
	}
}
