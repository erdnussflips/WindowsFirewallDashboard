using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFirewallDashboard.View;

namespace WindowsFirewallDashboard.ApplicationSystem
{
	public class NotificationManager
	{
		[STAThread]
		public void ShowWindow()
		{
			var window = new MainWindow();
			window.Show();
		}
	}
}
