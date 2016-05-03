using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Events;
using WindowsFirewallDashboard.Library.ApplicationSystem;
using WindowsFirewallDashboard.ViewModel.Base;

namespace WindowsFirewallDashboard.ViewModel
{
    class SettingsViewModel : BaseViewModel
    {
        public FirewallEventManager.InstallationStatus InstallationStatus
        {
            get
            {
                return FirewallEventManager.Instance.GetInstallationStatus();
            }
        }

        public FirewallProfile CurrentProfile => ApplicationManager.Instance.Firewall.CurrentProfile;

        public FirewallProfile PrivateProfile => ApplicationManager.Instance.Firewall.PrivateProfile;
        public FirewallProfile PublicProfile => ApplicationManager.Instance.Firewall.PublicProfile;
        public FirewallProfile DomainProfile => ApplicationManager.Instance.Firewall.DomainProfile;

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
