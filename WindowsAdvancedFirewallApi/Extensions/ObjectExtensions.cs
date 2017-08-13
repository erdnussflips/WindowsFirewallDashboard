using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Extensions
{
	public static class ObjectExtensions
	{
		public static bool Equals(this object value, params object[] valuesToCheck)
		{
			bool equals = false;

			foreach (var item in valuesToCheck)
			{
				equals = equals || value.Equals(item);
			}

			return equals;
		}
	}
}
