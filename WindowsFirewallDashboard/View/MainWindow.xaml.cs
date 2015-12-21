using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using WindowsAdvancedFirewallApi;
using WindowsAdvancedFirewallApi.Events;
using WindowsFirewallDashboard.Library.ApplicationSystem;
using System.Diagnostics;

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			InitializeCustomComponents();
			InitializeEvents();
		}

		private void InitializeCustomComponents()
		{
			ApplicationManager.Instance.Tray.RootWindow = this;
		}


		private void InitializeEvents()
		{
			tabEvents.GotFocus += TabEvents_GotFocus;
			ApplicationManager.Instance.Firewall.EventManager.HistoryLoaded += EventManager_HistoryLoaded;
		}

		private void TabEvents_GotFocus(object sender, RoutedEventArgs e)
		{
			ApplicationManager.Instance.Firewall.EventManager.LoadEventHistory();
		}

		private void EventManager_HistoryLoaded(object sender, List<WindowsAdvancedFirewallApi.Events.Arguments.FirewallBaseEventArgs> events)
		{
			eventHistory.ItemsSource = events;
		}

		private void EnableFirewall_Click(object sender, RoutedEventArgs e)
		{
		}

		private void DisableFirewall_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}
