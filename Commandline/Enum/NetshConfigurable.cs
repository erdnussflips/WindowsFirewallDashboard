using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum
{
	public class NetshConfigurable<T> : NetshParameter where T : NetshConfigurable<T>, new()
	{
		public static T NotConfigured = new T { value = "notconfigured" };
	}
}
