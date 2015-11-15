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
using WindowsAdvancedFirewallApi;
using WindowsAdvancedFirewallApi.Events;

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		private System.Windows.Forms.NotifyIcon notifyIcon;

		public MainWindow()
		{
			InitializeComponent();
			InitializeCustomComponents();
			InitializeEvents();

			StartListening();
			button.Click += Button_Click;
			button1.Click += Button1_Click;
			button2.Click += Button2_Click;

			FirewallEventManager.Instance.SettingsChanged += Instance_SettingsChanged;
		}

		private void Instance_SettingsChanged(object sender, WindowsAdvancedFirewallApi.Events.Arguments.FirewallSettingEventArgs e)
		{
			System.Windows.MessageBox.Show("Firewall settings changed");
		}

		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			StartListening();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			FirewallEventManager.Instance.Install();
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			FirewallEventManager.Instance.Deinstall();
		}

		private void InitializeCustomComponents()
		{
			Uri TweetyUri = new Uri(@"/Ressources/Images/NotifyIcon.ico", UriKind.Relative);
			System.IO.Stream IconStream = Application.GetResourceStream(TweetyUri).Stream;

			notifyIcon = new System.Windows.Forms.NotifyIcon();
			notifyIcon.Icon = new System.Drawing.Icon(IconStream);
			notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
			StateChanged += Window_StateChanged;
		}

		private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			WindowState = WindowState.Normal;
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			if (this.WindowState == WindowState.Minimized)
			{
				this.ShowInTaskbar = false;
				notifyIcon.Text = "Windows Firewall Dashboard";
				notifyIcon.Visible = true;
			}
			else if (this.WindowState == WindowState.Normal)
			{
				notifyIcon.Visible = false;
				this.ShowInTaskbar = true;
			}
		}

		private void InitializeEvents()
		{
			BtnDashboard.Click += (object sender, RoutedEventArgs e) =>
			{
				//viewModel.ShowDashboardCommand.Execute(null);
			};
		}

		private void StartListening()
		{
			if(FirewallEventManager.Instance.StartListingFirewall())
				this.TbNofitications.Text = "Benachrichtigungen aktiv.";
		}

		private void StopListening()
		{
			if(FirewallEventManager.Instance.StopListingFirewall())
				this.TbNofitications.Text = "Benachrichtigungen inaktiv.";
		}
	}
}
