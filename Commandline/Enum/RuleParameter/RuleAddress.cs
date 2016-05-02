using NetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.RuleParameter
{
	public class RuleAddress<T> : NetshParameter where T : RuleAddress<T>, new()
	{
		public static T Any = new T { value = "any" };

		public static T Custom(string addressString)
		{
			IPAddressRange address;
			var validAddress = IPAddressRange.TryParse(addressString, out address);

			if (!validAddress)
			{
				throw new ArgumentOutOfRangeException("The given address is not valid");
			}

			var addressParts = addressString.Split('/').ToList();
			var isSubnet = addressParts.Count == 2 ? true : false;

			IPAddress ip = null;
			IPAddress subnet = null;

			IPAddress.TryParse(addressParts.ElementAt(0), out ip);

			/*
			if (isSubnet)
			{
				int subnetMaskCount;
				var isMaskCount = int.TryParse(addressParts.ElementAt(1), out subnetMaskCount);
			}
			*/

			return new T { value = addressString, Ip = ip };
		}

		public static T Custom(string rangeBegin, string rangeEnd)
		{
			return new T();
		}

		private IPAddress Ip;
		private IPAddress Subnet;
	}
}
