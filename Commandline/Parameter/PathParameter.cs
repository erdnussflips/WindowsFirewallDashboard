using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter
{
	public class PathParameter : NetshSimpleParameter
	{
		public static PathParameter Custom(Uri path)
		{
			return new PathParameter { PathValue = path, Value = path.AbsolutePath };
		}

		public Uri PathValue { get; private set; }
	}
}
