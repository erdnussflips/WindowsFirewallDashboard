using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public class ProfileParameter : NetshSimpleParameter
	{
		public static readonly ProfileParameter Current = new ProfileParameter { Value = "currentprofile", UsableInRule = false };

		public static readonly ProfileParameter All = new ProfileParameter { Value = "allprofiles", ValueInRule = "any" };
		public static readonly ProfileParameter Public = new ProfileParameter { Value = "publicprofile", ValueInRule = "public" };
		public static readonly ProfileParameter Domain = new ProfileParameter { Value = "domainprofile", ValueInRule = "domain" };
		public static readonly ProfileParameter Private = new ProfileParameter { Value = "privateprofile", ValueInRule = "private" };

		public static readonly ProfileParameter DefaultInRule = All;

		protected internal bool UsableInRule = true;
		protected internal string ValueInRule { get; protected set; }

		protected internal readonly string NameInRule = "profile";
	}
}
