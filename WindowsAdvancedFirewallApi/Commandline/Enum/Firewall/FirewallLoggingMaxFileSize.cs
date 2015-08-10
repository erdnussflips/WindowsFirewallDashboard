using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallLoggingMaxFilesize : NetshConfigurable<FirewallLoggingMaxFilesize>
	{
		public const string Name = "maxfilesize";

		public static FirewallLoggingMaxFilesize Default = Custom(4096);
		public static FirewallLoggingMaxFilesize DefaultForGPO = NotConfigured;

		public static FirewallLoggingMaxFilesize Custom(short maxfilesize)
		{
			if(maxfilesize < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(maxfilesize), "The parameter must between 1 to 32767.");
			}

			return new FirewallLoggingMaxFilesize { size = maxfilesize, value = maxfilesize.ToString() };
		}

		private short size;
	}
}
