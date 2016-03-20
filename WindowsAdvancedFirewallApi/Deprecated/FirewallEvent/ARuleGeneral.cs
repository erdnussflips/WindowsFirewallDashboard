using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallLibrary.FirewallEvent
{
	[Serializable]
	public abstract class ARuleGeneral
	{
		// EventID: 2004, 2005, 2006

		public string RuleId;

		public string RuleName;

		public string ModifyingUser;

		public string ModifyingApplication;
	}
}
