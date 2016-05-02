using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Data
{
	public sealed class FirewallProtocol : IComparable
	{
		public enum ProtocolName
		{
			Custom, HOPOPT, ICMPv4, IGMP, TCP, UDP, IPv6, IPv6_Route, IPv6_Flag, GRE,
			ICMP_v6, IPv6_NoNxt, IPv6_Opts, VRRP, PGM, L2TP, All
		}

		public ProtocolName Name { get; private set; }
		public short Value { get; private set; }

		public FirewallProtocol(short value)
		{
			Name = ProtocolName.Custom;
			Value = value;
		}

		private FirewallProtocol(short value, ProtocolName name)
		{
			Name = name;
			Value = value;
		}

		public override string ToString()
		{
			return Enum.GetName(typeof(ProtocolName), Name);
		}

		public int CompareTo(object obj)
		{
			return ToString().CompareTo(obj?.ToString());
		}

		public static readonly FirewallProtocol HOPOPT = new FirewallProtocol(0, ProtocolName.HOPOPT);
		public static readonly FirewallProtocol ICMPv4 = new FirewallProtocol(1, ProtocolName.ICMPv4);
		public static readonly FirewallProtocol IGMP = new FirewallProtocol(2, ProtocolName.IGMP);
		public static readonly FirewallProtocol TCP = new FirewallProtocol(6, ProtocolName.TCP);
		public static readonly FirewallProtocol UDP = new FirewallProtocol(17, ProtocolName.UDP);
		public static readonly FirewallProtocol IPv6 = new FirewallProtocol(41, ProtocolName.IPv6);
		public static readonly FirewallProtocol IPv6_Route = new FirewallProtocol(43, ProtocolName.IPv6_Route);
		public static readonly FirewallProtocol IPv6_Flag = new FirewallProtocol(44, ProtocolName.IPv6_Flag);
		public static readonly FirewallProtocol GRE = new FirewallProtocol(47, ProtocolName.GRE);
		public static readonly FirewallProtocol ICMP_v6 = new FirewallProtocol(58, ProtocolName.ICMP_v6);
		public static readonly FirewallProtocol IPv6_NoNxt = new FirewallProtocol(59, ProtocolName.IPv6_NoNxt);
		public static readonly FirewallProtocol IPv6_Opts = new FirewallProtocol(60, ProtocolName.IPv6_Opts);
		public static readonly FirewallProtocol VRRP = new FirewallProtocol(112, ProtocolName.VRRP);
		public static readonly FirewallProtocol PGM = new FirewallProtocol(113, ProtocolName.PGM);
		public static readonly FirewallProtocol L2TP = new FirewallProtocol(115, ProtocolName.L2TP);
		public static readonly FirewallProtocol All = new FirewallProtocol(256, ProtocolName.All);

		public static readonly IList<FirewallProtocol> DefaultProtocols = new List<FirewallProtocol> { HOPOPT, ICMPv4, IGMP, TCP, UDP, IPv6, IPv6_Route, IPv6_Flag, GRE, ICMP_v6, IPv6_NoNxt, IPv6_Opts, VRRP, PGM, L2TP, All };

		public static FirewallProtocol Custom(short protocolValue)
		{
			return new FirewallProtocol(protocolValue);
		}
	}

	internal static class FirewallProtocolUtil
	{
		public static FirewallProtocol ToFirewallProtocol(this short value)
		{
			foreach (var protocol in FirewallProtocol.DefaultProtocols)
			{
				if (protocol.Value == value) return protocol;
			}

			return new FirewallProtocol(value);
		}

		public static FirewallProtocol ToFirewallProtocol(this int value) => ToFirewallProtocol((short)value);
	}
}
