using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore
{
	public class CoreConstants
	{
		public static string PipeEndpoint = "net.pipe://localhost/" + nameof(WindowsFirewallCore);
	}
}
