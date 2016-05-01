using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Data
{
	public sealed class FirewallPorts : IComparable
	{
		public enum Types {
			Unknown, All, Specific, RPC, RPC_EPMap, IP_HTTPS
		}

		public Types Type { private set; get; }
		public string Native { private set; get; }
		private List<int> Ports = new List<int>();

		private FirewallPorts(Types type, string native)
		{
			Type = type;
			Native = native;
		}

		public FirewallPorts(params int[] ports) : this(Types.Specific, null)
		{
			Ports.AddRange(ports);
		}

		public override string ToString()
		{
			if (Type == Types.Specific)
			{
				return Ports.Stringify();
			}

			return Type.ToString();
		}

		public int CompareTo(object obj)
		{
			return ToString().CompareTo(obj?.ToString());
		}

		public static readonly FirewallPorts All = new FirewallPorts(Types.All, "*");
		// dynamic rpc ports
		public static readonly FirewallPorts RPC = new FirewallPorts(Types.RPC, "RPC");
		// rpc endpoint classification
		public static readonly FirewallPorts RPC_EPMap = new FirewallPorts(Types.RPC_EPMap, "RPC-EPMap");
		// ip https
		public static readonly FirewallPorts IPHTTPS = new FirewallPorts(Types.IP_HTTPS, "IPHTTPS");
		public static readonly FirewallPorts mDNS = new FirewallPorts(Types.IP_HTTPS, "mDNS");
		public static readonly FirewallPorts Teredo = new FirewallPorts(Types.IP_HTTPS, "Teredo");
		public static readonly FirewallPorts Ply2Disc = new FirewallPorts(Types.IP_HTTPS, "Ply2Disc");

		public static readonly IList<FirewallPorts> Predefined = new List<FirewallPorts> { All, RPC, RPC_EPMap, IPHTTPS, mDNS, Teredo, Ply2Disc };

		public static FirewallPorts Specific(params int[] ports)
		{
			return new FirewallPorts(ports);
		}

		internal static FirewallPorts CurrentlyNotImplemented(string native)
		{
			return new FirewallPorts(Types.Unknown, native);
		}
	}

	internal static class FirewallPortsUtils
	{
		public static FirewallPorts ToFirewallPorts(this string value)
		{
			var trimmedValue = value?.TrimEnd(',');
			var splittedPorts = trimmedValue?.Split(',');

			if (splittedPorts == null || splittedPorts.Length < 1)
			{
				return null;
			}

			var isNumber = !splittedPorts.Any(port => !port.IsNumber());
			if (isNumber)
			{
				var ports = splittedPorts.Select(port => port.ParseInteger());
				return new FirewallPorts(ports.ToArray());
			}

			foreach (var item in FirewallPorts.Predefined)
			{
				if (trimmedValue?.Equals(item.Native) ?? false)
				{
					return item;
				}
			}

			return FirewallPorts.CurrentlyNotImplemented(trimmedValue);
		}

		public static string ToNativeValue(this FirewallPorts ports)
		{
			return ports.ToString();
		}
	}
}
