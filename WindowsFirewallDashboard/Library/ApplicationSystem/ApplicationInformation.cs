using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class ApplicationInformation
	{
		public static Assembly GetExecutingAssembly()
		{
			return System.Reflection.Assembly.GetExecutingAssembly();
		}

		public static string GetApplicationFilename()
		{
			return System.AppDomain.CurrentDomain.FriendlyName;
		}

		public static string GetApplicationName()
		{
			return nameof(WindowsFirewallDashboard);
			//return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
		}

		public static string GetApplicationFileExecutionPath()
		{
			return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
		}
	}
}
