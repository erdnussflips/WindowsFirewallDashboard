using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Utils
{
	public static class RegExTool
	{
		public static readonly string FLOATING_POINT_NUMBER = @"[+-]?\d+(?:\.\d+)?";

		public static Match Match(this string value, string regex)
		{
			return new Regex(regex).Match(value);
		}

		public static MatchCollection Matches(this string value, string regex)
		{
			return new Regex(regex).Matches(value);
		}
	}
}
