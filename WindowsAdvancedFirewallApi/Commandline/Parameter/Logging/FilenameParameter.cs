using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Logging
{
	public class FilenameParameter : LoggingParameter<FilenameParameter, FilenameParameter.Value>
	{
		public class Value : NetshConfigurableValue<Value>
		{
			public static Value Default = new Value { NetshValue = new Uri(@"%windir%\system32\logfiles\firewall\pfirewall.log", UriKind.RelativeOrAbsolute).AbsolutePath };
			public static Value DefaultForGPO = NotConfigured;

			public static Value Custom(Uri filepath)
			{
				return new Value { NetshValue = filepath.AbsolutePath, userdefined = true };
			}

			private bool userdefined = false;

			internal bool FilefolderNeedsPermission()
			{
				return userdefined;
			}
		}
	}
}
