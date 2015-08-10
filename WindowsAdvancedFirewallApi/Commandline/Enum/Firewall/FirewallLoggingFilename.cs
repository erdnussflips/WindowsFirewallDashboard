using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum.Firewall
{
	public class FirewallLoggingFilename : NetshConfigurable<FirewallLoggingFilename>
	{
		public const string Name = "filename";

		public static FirewallLoggingFilename Default = new FirewallLoggingFilename { value = @"%windir%\system32\logfiles\firewall\pfirewall.log" };
		public static FirewallLoggingFilename DefaultForGPO = NotConfigured;

		public static FirewallLoggingFilename Custom(string filepath)
		{
			return new FirewallLoggingFilename { value = filepath, custom = true };
		}

		private bool custom = false;

		internal bool FolderNeedsPermission()
		{
			return custom;
		}
	}
}
