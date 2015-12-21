using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallProfileSetting : FirewallSetting
	{
		public Profile Profiles { get; set; }
	}
}
