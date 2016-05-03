using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore
{
	public static class CoreConstants
	{
		public static readonly string PipeEndpoint = "net.pipe://localhost/" + nameof(WindowsFirewallCore);
	}
}
