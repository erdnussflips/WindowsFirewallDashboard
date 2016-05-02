using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.RuleParameter
{
	public class RuleService : NetshParameter
	{
		public static RuleService Any = new RuleService { value = "any" };
		public static RuleService Custom(string serviceShortName)
		{
			return new RuleService { value = serviceShortName };
		}
	}
}
