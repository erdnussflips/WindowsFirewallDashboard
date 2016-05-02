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
			public static Value Default = new Value { Value = new Uri(@"%windir%\system32\logfiles\firewall\pfirewall.log", UriKind.RelativeOrAbsolute).AbsolutePath };
			public static Value DefaultForGPO = NotConfigured;

			public static Value Custom(Uri filepath)
			{
				return new Value { Value = filepath.AbsolutePath, userdefined = true };
			}

			private bool userdefined;

			internal bool FilefolderNeedsPermission()
			{
				return userdefined;
			}
		}

		public FilenameParameter(Value value) : base("filename", value) { }
		public FilenameParameter() : this(null) { }
	}
}
