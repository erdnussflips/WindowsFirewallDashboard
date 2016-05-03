using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public class StateParameter : NetshConfigurableParameter<StateParameter>
	{
		public static readonly StateParameter On = new StateParameter { Value = "on" };
		public static readonly StateParameter Off = new StateParameter { Value = "off" };

		public static readonly StateParameter Default = On;
		public static readonly StateParameter DefaultForServer2008 = On;
	}
}
