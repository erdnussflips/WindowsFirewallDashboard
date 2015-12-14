using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFirewallDashboard.Library.Utils
{
    public static class GeneralUtils
    {
        public static void ShowContextMenu(this NotifyIcon notifyIcon)
        {
            notifyIcon.GetType().InvokeMember("ShowContextMenu", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, notifyIcon, null);
        }
    }
}
