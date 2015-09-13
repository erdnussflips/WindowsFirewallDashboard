using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.COM.Types
{
	public class COMWrapperType<COMType>
	{
		protected internal COMType COMObject { get; protected set; }

		public COMWrapperType()
		{
		}

		protected COMWrapperType(Type type)
		{
			COMObject = (COMType)Activator.CreateInstance(type);
		}

		protected COMWrapperType(string progID)
		{
			COMObject = (COMType)Activator.CreateInstance(Type.GetTypeFromProgID(progID));
		}
	}
}
