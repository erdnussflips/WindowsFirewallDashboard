using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Extensions
{
	public static class DebuggerExtensions
	{
		private static bool BREAK_ENABLED = false;

		public static void Break()
		{
			if (BREAK_ENABLED)
			{
				Debugger.Break();
			}
		}
	}
}
