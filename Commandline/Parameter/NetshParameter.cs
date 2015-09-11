using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public abstract class NetshParameter
	{
		internal NetshParameter()
		{

		}

		public abstract override string ToString();
	}
}
