using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsFirewallDashboard.View;

namespace WindowsFirewallDashboard.ApplicationSystem
{
	public class NotificationManager
	{
		[STAThread]
		private void ShowWindow(Type t)
		{
			
		}

		[STAThread]
		public void ShowNewMainWindow()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				var window = new MainWindow();
				window.Show();
			});
		}

		[STAThread]
		public void ShowEventNotification()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				var window = new FirewallEventNotificationWindow();
				window.Show();
			});
		}
	}
}
