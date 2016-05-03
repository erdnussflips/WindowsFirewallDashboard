using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Library.Interfaces
{
    interface INotificationManager
    {
        [STAThread]
        void ShowEventNotification();
    }
}
