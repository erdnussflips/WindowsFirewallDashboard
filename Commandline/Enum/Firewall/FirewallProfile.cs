using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallProfile : NetshParameter
	{
		public static FirewallProfile Current = new FirewallProfile { value = "currentprofile", useableInRules = false };

		public static FirewallProfile All = new FirewallProfile { value = "allprofiles", valueForRule = "any" };
		public static FirewallProfile Domain = new FirewallProfile { value = "domainprofile", valueForRule = "domain" };
		public static FirewallProfile Private = new FirewallProfile { value = "privateprofile", valueForRule = "private" };
		public static FirewallProfile Public = new FirewallProfile { value = "publicprofile", valueForRule = "public" };

		private bool useableInRules = true;

		public string valueForRule { get; protected set; }

		public bool IsUsbaleInRules()
		{
			return useableInRules;
		}
	}
}
