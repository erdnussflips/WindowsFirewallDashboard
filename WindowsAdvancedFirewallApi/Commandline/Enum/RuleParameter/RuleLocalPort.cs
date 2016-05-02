using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.RuleParameter
{
	public class RuleLocalPort : RulePort<RuleLocalPort>
	{
		public static RuleLocalPort Rpc = new RuleLocalPort { value = "rpc" };
		public static RuleLocalPort PpcEpmap  = new RuleLocalPort { value = "rpc-epmap" };
		public static RuleLocalPort IpHttps = new RuleLocalPort { value = "iphttps" };
		public static RuleLocalPort Teredo = new RuleLocalPort { value = "teredo" };
	}
}
