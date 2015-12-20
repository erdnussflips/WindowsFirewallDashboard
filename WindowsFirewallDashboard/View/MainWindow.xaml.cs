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

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			InitializeCustomComponents();
			InitializeEvents();

			StartListening();
		}

		private void InitializeCustomComponents()
		{
			ApplicationManager.Instance.Tray.RootWindow = this;
		}

		private void InitializeEvents()
		{

		}

		private void StartListening()
		{
			//if(FirewallEventManager.Instance.StartListingFirewall())
				//this.TbNofitications.Text = "Benachrichtigungen aktiv.";
		}

		private void StopListening()
		{
			//if(FirewallEventManager.Instance.StopListingFirewall())
				//this.TbNofitications.Text = "Benachrichtigungen inaktiv.";
		}
	}
}
