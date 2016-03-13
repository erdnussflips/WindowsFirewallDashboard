using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFirewallDashboard.Library.Utils
{
	static class GeneralUtils
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		public static void ShowContextMenu(this NotifyIcon notifyIcon)
		{
			try
			{
				notifyIcon.GetType().InvokeMember("ShowContextMenu", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, notifyIcon, null);
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}
		}
	}
}
