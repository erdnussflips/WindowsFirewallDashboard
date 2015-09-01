using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.Logging
{
	public class MaxFilesizeParameter : NetshExtendedParameter<MaxFilesizeParameter, MaxFilesizeParameter.Value>
	{
		public class Value : NetshConfigurableValue<Value>
		{
			public static Value Default = Custom(4096);
			public static Value DefaultForGPO = NotConfigured;

			public static Value Custom(short maxfilesize)
			{
				if (maxfilesize < 1)
				{
					throw new ArgumentOutOfRangeException(nameof(maxfilesize), "The parameter value must between 1 to 32767.");
				}

				return new Value { Value = maxfilesize.ToString(), Size = maxfilesize };
			}

			private short Size;
		}

		public MaxFilesizeParameter() : base("maxfilesize")
		{

		}
	}
}
