using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Enum
{
	public abstract class NetshParameter
	{
		public string value { get; protected set; }

		protected NetshParameter()
		{

		}
	}
}
