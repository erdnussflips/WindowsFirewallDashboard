using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public class StateParameter : NetshConfigurableParameter<StateParameter>
	{
		public static StateParameter On = new StateParameter { Value = "on" };
		public static StateParameter Off = new StateParameter { Value = "off" };
	}
}
