using NLog;
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
using WindowsAdvancedFirewallApi.Events;
using WindowsAdvancedFirewallApi.Utils;
using WindowsFirewallDashboard.Library.Utils;
using WindowsFirewallDashboard.Locator;
using WindowsFirewallDashboard.ViewModel;

namespace WindowsFirewallDashboard.View.Components
{
    /// <summary>
    /// Interaktionslogik für SettingsGrid.xaml
    /// </summary>
    public partial class SettingsGrid : UserControl
    {
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private SettingsViewModel ViewModel => ViewModelLocator.Settings;

        public SettingsGrid()
        {
            InitializeComponent();
            InitializeView();
        }

        private void InitializeView()
        {
            checkBoxAutostart.Click += CheckBoxAutostart_Click;
            checkBoxShellIntegration.Click += CheckBoxShellIntegration_Click;
        }

        private void CheckBoxShellIntegration_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon");

            checkBoxShellIntegration.IsChecked = false;
            return;

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
    }
}
