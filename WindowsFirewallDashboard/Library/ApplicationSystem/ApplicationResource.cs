using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class ApplicationResource
	{
		public static Uri NotifyIconBlack = new Uri(@"/Resources/Images/NotifyIcon-black.ico", UriKind.Relative);
		public static Uri NotifyIconWhite = new Uri(@"/Resources/Images/NotifyIcon-white.ico", UriKind.Relative);
		public static Uri NotifyIconGreen = new Uri(@"/Resources/Images/NotifyIcon-green.ico", UriKind.Relative);
		public static Uri NotifyIconOrange = new Uri(@"/Resources/Images/NotifyIcon-orange.ico", UriKind.Relative);
		public static Uri NotifyIconRed = new Uri(@"/Resources/Images/NotifyIcon-red.ico", UriKind.Relative);
		public static Uri NotifyIconBlue = new Uri(@"/Resources/Images/NotifyIcon-blue.ico", UriKind.RelativeOrAbsolute);

		public static string GetFilename(Uri resource)
		{
			return resource.Segments[resource.Segments.Count() - 1];
		}

		public static Stream AsStreamFromApplication(Uri resource)
		{
			return Application.GetResourceStream(resource).Stream;
		}

		public static Stream AsStreamFromAssembly(Uri resource)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetFilename(NotifyIconBlue));
		}
	}
}
