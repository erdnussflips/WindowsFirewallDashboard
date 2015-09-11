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
	}
}
