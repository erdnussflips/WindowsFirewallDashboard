using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class COMWrapperType<COMType>
	{
		protected COMType COMObject;

		public COMWrapperType()
		{
		}

		public COMWrapperType(string progID)
		{
			COMObject = (COMType)Activator.CreateInstance(Type.GetTypeFromProgID(progID));
		}
	}
}
