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
using System.Collections.ObjectModel;
using WindowsAdvancedFirewallApi.Library;
using WindowsFirewallDashboard.Locator;
using WindowsFirewallDashboard.ViewModel;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	partial class MainWindow : MetroWindow
	{
		private bool eventListFirstShown = false;

		private MainViewModel ViewModel => ViewModelLocator.Main;

		public MainWindow()
		{
			InitializeComponent();
			InitializeView();
		}

		private void InitializeView()
		{
			ViewModel.RootWindow = this;
			if ((bool)ApplicationManager.Instance.StartupOptions?.StartMinimized)
			{
				WindowState = WindowState.Minimized;
				ShowInTaskbar = false;
				Hide();
			}

			ViewModel.HistoryLoaded += ViewModel_HistoryLoaded;
			ViewModel.HistoryLoadingStatusChanged += ViewModel_HistoryLoadingStatusChanged;

			tabEvents.GotFocus += TabEvents_GotFocus;
			settingsButton.Click += SettingsButton_Click;
			checkBoxAutostart.Click += CheckBoxAutostart_Click;
			checkBoxShellIntegration.Click += CheckBoxShellIntegration_Click;
		}

		private void CheckBoxShellIntegration_Click(object sender, RoutedEventArgs e)
		{
			if ((bool)checkBoxShellIntegration.IsChecked)
			{
				ViewModel.EnableShellIntegration();
			}
			else
			{
				ViewModel.DisableShellIntegration();
			}
		}

		private void CheckBoxAutostart_Click(object sender, RoutedEventArgs e)
		{
			if ((bool)checkBoxAutostart.IsChecked)
			{
				ViewModel.EnableAutostart();
			}
			else
			{
				ViewModel.DisableAutostart();
			}
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			//MainControl.SelectedItem = tabSettings;
			settingsFlyout.IsOpen = !settingsFlyout.IsOpen;
			labelInstallState.Content = EnumUtils.GetEnumValueName<FirewallEventManager.InstallationStatus>(ViewModel.InstallationStatus);
		}

		private void TabEvents_GotFocus(object sender, RoutedEventArgs e)
		{
			EventListFirstShown();
		}

		private void EventListFirstShown()
		{
			if (!eventListFirstShown)
			{
				eventListFirstShown = true;
				ViewModel.LoadEventHistory();
			}
		}

		private void ViewModel_HistoryLoadingStatusChanged(object sender, FirewallHistoryLoadingStatusChangedEventArgs e)
		{
			Dispatcher.BeginInvoke(new Action(() => {
				loadingLabel.Content = e.LoadedCount + " Ereignis(se) von " + e.MaxCount + " geladen";
			}), null);
		}

		private void ViewModel_HistoryLoaded(object sender, ICollection<FirewallBaseEventArgs> events)
		{
			Dispatcher.BeginInvoke(new Action(() => {
				// loadingLabel.Visibility = Visibility.Collapsed;
				loadingLabel.Content = events.Count + " geladen";
			}), null);
		}

		private void EnableFirewall_Click(object sender, RoutedEventArgs e)
		{
		}

		private void DisableFirewall_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}
