using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.WindowsRegistry
{
    class RegistryHelper
    {
        public static readonly RegistryHelper Local = new RegistryHelper();

        private RegistryKey RootKey { get; set; }

        public RuleMatcher RuleMatcher { get; private set; }

        private RegistryHelper(RegistryHive hive, string machineName = null)
        {
            if (machineName != null)
            {
                RootKey = RegistryKey.OpenRemoteBaseKey(hive, machineName, RegistryView.Default);
            }
            else
            {
                RootKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default);
            }

            RuleMatcher = new RuleMatcher(RootKey);
        }

        public RegistryHelper(string machineName = null) : this(RegistryHive.LocalMachine, machineName)
        {

        }

        public void EnableEventLog()
        {
            RootKey.CreateSubKey(ApiConstants.REGISTRY_KEY_FIREWALL_LOG);
        }

        public void DisabledEventLog()
        {
            RootKey.DeleteSubKey(ApiConstants.REGISTRY_KEY_FIREWALL_LOG);
        }

        public bool IsEventLogEnabled()
        {
            using (var key = RootKey.OpenSubKey(ApiConstants.REGISTRY_KEY_FIREWALL_LOG))
            {
                if (key == null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
