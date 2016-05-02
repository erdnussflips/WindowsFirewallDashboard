using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.RuleParameter
{
	public class RuleAttributes
	{
		public enum Activatable
		{
			Yes, No
		}

		public enum Direction
		{
			In, Out
		}

		public enum Action
		{
			Allow, Block, Bypass
		}

		public enum InterfaceType { Any, Wireless, Lan, Ras }

		public enum Edge { Yes, DeferApp, DeferUser, No }

		public enum Security { Authenticate, AuthEnc, AuthDynEnc, AuthNoEncap, NotRequired }

	}
}
