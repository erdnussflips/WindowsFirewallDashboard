using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.RuleParameter
{
	public class RulePort<T> : NetshParameter where T : RulePort<T>, new()
	{
		public static T Any = new T { value = "any" };

		public static T Custom(int portnumber)
		{
			if(portnumber < 0 || portnumber > 65535)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new T { value = portnumber.ToString() };
		}

		public static T Custom(int portStart, int portEnd)
		{

		}

		public static T Custom(params int[] ports)
		{

		}
	}
}
