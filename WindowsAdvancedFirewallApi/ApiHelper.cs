using NLog;
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
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		[DllImport("shell32.dll", EntryPoint = "IsUserAnAdmin")]
		public static extern bool HasAdministratorPrivileges();

		public static void RaiseExceptionOnUnauthorizedAccess(string customMessage = null, bool append = false)
		{
			if(!HasAdministratorPrivileges())
			{
				const string messageNormal = "You need administrator rights for this action.";
				const string messageAppend = "You need administrator rights ";
				var message = string.Empty;

				message = customMessage != null ? (append ? messageAppend + customMessage : customMessage) : messageNormal;

				var ex = new UnauthorizedAccessException(message);
				LOG.Debug(ex.Message);
				throw ex;
			}
		}
	}
}
