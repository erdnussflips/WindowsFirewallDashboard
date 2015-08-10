using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public class ProfileParameter : NetshSimpleParameter
	{
		public static ProfileParameter Current = new ProfileParameter { Value = "currentprofile" };

		public static ProfileParameter All = new ProfileParameter { Value = "allprofiles" };
		public static ProfileParameter Public = new ProfileParameter { Value = "publicprofile" };
		public static ProfileParameter Domain = new ProfileParameter { Value = "domainprofile" };
		public static ProfileParameter Private = new ProfileParameter { Value = "privateprofile" };
	}
}
