using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Events.Arguments;
using WindowsFirewallDashboard.Library.ApplicationSystem;
using WindowsFirewallDashboard.ViewModel.Base;

namespace WindowsFirewallDashboard.ViewModel
{
	class MainViewModel : BaseViewModel
	{
		public Window RootWindow
		{
			set
			{
				ApplicationManager.Instance.WindowManager.RootWindow = value;
			}
			get
			{
				return ApplicationManager.Instance.WindowManager.RootWindow;
			}
		}

		public FirewallEventManager.InstallationStatus InstallationStatus
		{
			get
			{
				return FirewallEventManager.Instance.GetInstallationStatus();
			}
		}

		public ICollection<FirewallBaseEventArgs> EventHistory
		{
			get
			{
				return ApplicationManager.Instance.Firewall.EventManager.History;
			}
		}
		public event EventHandler<FirewallHistoryLoadingStatusChangedEventArgs> HistoryLoadingStatusChanged
		{
			add
			{
				ApplicationManager.Instance.Firewall.EventManager.HistoryLoadingStatusChanged += value;
			}
			remove
			{
				ApplicationManager.Instance.Firewall.EventManager.HistoryLoadingStatusChanged -= value;
			}
		}
		public event EventHandler<ICollection<FirewallBaseEventArgs>> HistoryLoaded
		{
			add
			{
				ApplicationManager.Instance.Firewall.EventManager.HistoryLoaded += value;
			}
			remove
			{
				ApplicationManager.Instance.Firewall.EventManager.HistoryLoaded -= value;
			}
		}
		public void LoadEventHistory() => ApplicationManager.Instance.Firewall.EventManager.LoadEventHistory();

		public bool IsAutostartEnabled
		{
			get
			{
				return ApplicationManager.Instance.Installer.IsAutostartTaskInstalled();
			}
		}
		public void EnableAutostart()
		{
			ApplicationManager.Instance.Installer.InstallAutostartTask();
			RaiseOnPropertyChanged(nameof(IsAutostartEnabled));
		}
		public void DisableAutostart()
		{
			ApplicationManager.Instance.Installer.DeinstallAutostartTask();
			RaiseOnPropertyChanged(nameof(IsAutostartEnabled));
		}

		public bool? IsShellIntegrationEnabled
		{
			get
			{
				return ApplicationManager.Instance.Installer.IsShellIntegrationInstalled();
			}
		}
		public void EnableShellIntegration()
		{
			ApplicationManager.Instance.Installer.InstallShellIntegration();
			RaiseOnPropertyChanged(nameof(IsShellIntegrationEnabled));
		}
		public void DisableShellIntegration()
		{
			ApplicationManager.Instance.Installer.DeinstallShellIntegration();
			RaiseOnPropertyChanged(nameof(IsShellIntegrationEnabled));
		}
	}
}
