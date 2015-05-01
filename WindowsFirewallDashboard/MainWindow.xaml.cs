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
using WindowsFirewallHelper;
using MahApps.Metro.Controls;

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		private Win8Controller FwController = new Win8Controller();
		public MainWindow()
		{
			InitializeComponent();
			InitializeEvents();
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
			if (!Win8Controller.HasAdministratorPrivileges())
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
