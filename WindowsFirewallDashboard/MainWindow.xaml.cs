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
using WindowsFirewallLibrary.FirewallController;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using WindowsFirewallLibrary.Communication;

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		private Win8Controller FwController = new Win8Controller();
		private Server communicationServer;
		public MainWindow()
		{
			InitializeComponent();
			InitializeEvents();

			communicationServer = new Server();
		}

		public void InitializeEvents()
		{
			Loaded += MainWindow_Loaded;
			btnEnable.Click += BtnEnableOnClick;
			btnEnableDomain.Click += BtnEnableDomainOnClick;
			btnEnablePrivate.Click += BtnEnablePrivateOnClick;
			btnEnablePublic.Click += BtnEnablePublicOnClick;
			btnDisable.Click += BtnDisableOnClick;
			btnDisableDomain.Click += BtnDisableDomainOnClick;
			btnDisablePrivate.Click += BtnDisablePrivateOnClick;
			btnDisablePublic.Click += BtnDisablePublicOnClick;
			btnEnableNotifications.Click += BtnEnableNotifications_Click;
			btnDisableNofifications.Click += BtnDisableNofifications_Click;
			btnRestoreDefaults.Click += BtnRestoreDefaults_Click;
			btnStartListening.Click += BtnStartListening_Click;
			btnStopListening.Click += BtnStopListening_Click;
		}

		private void BtnStopListening_Click(object sender, RoutedEventArgs e)
		{
			communicationServer.Stop();
			MessageBox.Show("Listening disabled");
		}

		private void BtnStartListening_Click(object sender, RoutedEventArgs e)
		{
			communicationServer.Start();
			MessageBox.Show("Listening enabled");
		}

		private void BtnRestoreDefaults_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Do you really want to restore windows firewall default settings? This can not undo!");
			if (result == MessageBoxResult.OK || result == MessageBoxResult.Yes)
			{
				FwController.RestoreFirewallDefaults();
				MessageBox.Show("Firewall settings are restored");
			}
		}

		private async void BtnDisableNofifications_Click(object sender, RoutedEventArgs e)
		{
			if (FwController.DisableFirewallEventLogging())
			{
				await this.ShowMessageAsync("Success","Firewall event logging disabled.");
			}
			else
			{
				await this.ShowMessageAsync("Warning", "You must have admin rights!");
			}
		}

		private void BtnEnableNotifications_Click(object sender, RoutedEventArgs e)
		{
			if (FwController.EnableFirewallEventLogging())
			{
				MessageBox.Show("Firewall event logging enabled.");
			}
			else
			{
				MessageBox.Show("You must have admin rights!");
			}
		}

		private void BtnDisablePublicOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.DisableFirewallOnPublicNetworks();
		}

		private void BtnDisablePrivateOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.DisableFirewallOnPrivateNetworks();
		}

		private void BtnDisableDomainOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.DisableFirewallOnDomainNetworks();
		}

		private void BtnDisableOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.DisableFirewall();
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (!AController.HasAdministratorPrivileges())
			{
				MessageBox.Show("You must have admin privileges to change firewall settings!");
			}
		}

		private void BtnEnableOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.EnableFirewall();
		}

		private void BtnEnablePrivateOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.EnableFirewallOnPrivateNetworks();
		}

		private void BtnEnableDomainOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.EnableFirewallOnDomainNetworks();
		}

		private void BtnEnablePublicOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FwController.EnableFirewallOnPublicNetworks();
		}
	}
}
