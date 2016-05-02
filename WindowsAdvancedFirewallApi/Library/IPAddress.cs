using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Library
{
	/// <summary>Stellt eine Internetprotokolladresse (IP) bereit.</summary>
	public class IPAddress : System.Net.IPAddress, IComparable
	{

		public IPAddress(long newAddress) : base(newAddress) { }
		public IPAddress(byte[] address) : base(address) { }
		public IPAddress(byte[] address, long scopeid) : base(address, scopeid) { }

		public IPAddress(System.Net.IPAddress address) : this (address?.GetAddressBytes())
		{

		}

		public int CompareTo(object obj)
		{
			if (obj is IPAddress)
			{
				var castedObj = obj as IPAddress;

				var value = this.GetAddressValue();
				var objValue = castedObj.GetAddressValue();

				return value?.CompareTo(objValue.Value) ?? -1;
			}

			return -1;
		}

		public static bool operator <(IPAddress left, IPAddress right)
		{
			return left.GetAddressValue() < right.GetAddressValue();
		}

		public static bool operator >(IPAddress left, IPAddress right)
		{
			return left.GetAddressValue() > right.GetAddressValue();
		}

		public static bool operator <=(IPAddress left, IPAddress right)
		{
			return left.GetAddressValue() <= right.GetAddressValue();
		}

		public static bool operator >=(IPAddress left, IPAddress right)
		{
			return left.GetAddressValue() >= right.GetAddressValue();
		}
	}

	internal static class IPAddressUtils
	{
		public static IPAddress Parse(string address)
		{
			 System.Net.IPAddress ipAddress;

			if (System.Net.IPAddress.TryParse(address, out ipAddress))
			{
				return new IPAddress(ipAddress);
			}

			return null;
		}

		public static bool IsValid(string address)
		{
			return Parse(address) != null;
		}

		public static BigInteger? GetAddressValue(this System.Net.IPAddress address)
		{
			var addressBytes = address.GetAddressBytes();
			var addressValue = new BigInteger(0);

			var isIPv4 = addressBytes.Count() == 4;
			var isIPv6 = addressBytes.Count() == 16;

			var shiftBits = isIPv4 ? 8 : isIPv6 ? (int?)16 : null;

			if (shiftBits == null)
			{
				return null;
			}

			for (int i = 0, unitIndex = addressBytes.Count() - 1; i < addressBytes.Count(); i++, unitIndex--)
			{
				var @byte = addressBytes[i];

				var unitValue = new BigInteger(@byte);

				var shift = (int)(unitIndex * shiftBits);
				unitValue = (unitValue << shift);

				addressValue += unitValue;
			}

			return addressValue;
		}
	}
}
