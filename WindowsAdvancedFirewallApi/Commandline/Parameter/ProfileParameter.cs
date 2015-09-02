using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public class ProfileParameter : NetshSimpleParameter
	{
		public static ProfileParameter Current = new ProfileParameter { Value = "currentprofile", UsableInRule = false };

		public static ProfileParameter All = new ProfileParameter { Value = "allprofiles", ValueInRule = "any" };
		public static ProfileParameter Public = new ProfileParameter { Value = "publicprofile", ValueInRule = "public" };
		public static ProfileParameter Domain = new ProfileParameter { Value = "domainprofile", ValueInRule = "domain" };
		public static ProfileParameter Private = new ProfileParameter { Value = "privateprofile", ValueInRule = "private" };

		public static ProfileParameter DefaultInRule = All;

		protected internal bool UsableInRule = true;
		protected internal string ValueInRule { get; protected set; }
	}
}
