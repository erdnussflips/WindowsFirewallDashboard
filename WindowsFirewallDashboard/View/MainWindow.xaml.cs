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
		private Server communicationServer;
		public MainWindow()
		{
			InitializeComponent();
			InitializeEvents();

			communicationServer = new Server();
		}

		public void InitializeEvents()
		{
			BtnDashboard.Click += (object sender, RoutedEventArgs e) =>
			{
				//viewModel.ShowDashboardCommand.Execute(null);
			};
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
	}
}
