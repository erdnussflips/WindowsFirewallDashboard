using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class FirewallPolicy : COMWrapperType<INetFwPolicy2>
	{
		public FirewallPolicy() : base("HNetCfg.FwPolicy2") { }

		public void GetCurrentProfile()
		{

		}
	}
}
