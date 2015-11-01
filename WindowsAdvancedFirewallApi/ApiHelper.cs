using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi
{
	public static class ApiHelper
	{
		[DllImport("shell32.dll", EntryPoint = "IsUserAnAdmin")]
		public static extern bool HasAdministratorPrivileges();

		public static void RaiseExceptionOnUnauthorizedAccess(string customMessage = null, bool append = false)
		{
			if(!HasAdministratorPrivileges())
			{
				string messageNormal = "You need administrator rights for this action.";
				string messageAppend = "You need administrator rights ";
				string message = String.Empty;

				if(customMessage != null)
				{
					if(append)
					{
						 message = messageAppend + customMessage;
					}
					else
					{
						message = customMessage;
					}
				}
				else
				{
					message = messageNormal;
				}

				throw new UnauthorizedAccessException(message);
			}
		}
	}
}
