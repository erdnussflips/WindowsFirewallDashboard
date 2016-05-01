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
	static class CommonUtils
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

		public static bool ImplementsGenericInterface(this object value, Type interfaceType)
		{
			var interfaces = value.GetType().GetInterfaces();
			var genericTypes = interfaces.Where(i => i.IsGenericType);
			var result = genericTypes.Any(i => i.GetGenericTypeDefinition() == interfaceType);
			return result;
		}
	}
}
