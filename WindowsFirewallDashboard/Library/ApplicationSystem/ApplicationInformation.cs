using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			return Assembly.GetExecutingAssembly();
		}

		public static string GetApplicationFilename()
		{
			return AppDomain.CurrentDomain.FriendlyName;
		}

		public static string GetApplicationName()
		{
			return nameof(WindowsFirewallDashboard);
			//return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
		}

		public static string GetApplicationCompany()
		{
			var versionInfo = FileVersionInfo.GetVersionInfo(GetExecutingAssembly().Location);

			return versionInfo.CompanyName;

		}

		public static string GetApplicationFileInstallPath()
		{
			return new Uri(GetExecutingAssembly().CodeBase).LocalPath;
		}

		public static string GetApplicationFileExecutionPath()
		{
			return Process.GetCurrentProcess().MainModule.FileName;
		}
	}
}
