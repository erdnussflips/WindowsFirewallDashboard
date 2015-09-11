using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum
{
	public class NetshControllable<T> : NetshConfigurable<T> where T : NetshControllable<T>, new()
    {
		public static T Enable = new T { value = "enable" };
		public static T Disable = new T { value = "disable" };
	}
}
