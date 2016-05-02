using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallProfileSetting : FirewallSetting
	{
		public IList<FirewallProfileType> Profiles { get; set; }
	}
}
