using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.WindowsRegistry
{
    class RegistryManager
    {
        public static readonly RegistryManager Local = new RegistryManager();

        private RegistryKey RootKey { get; set; }

        public RuleManager RuleManagement { get; private set; }

        private RegistryManager(RegistryHive hive, string machineName = null)
        {
            if (machineName != null)
            {
                RootKey = RegistryKey.OpenRemoteBaseKey(hive, machineName, RegistryView.Default);
            }
            else
            {
                RootKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default);
            }

            RuleManagement = new RuleManager(RootKey);
        }

        public RegistryManager(string machineName = null) : this(RegistryHive.LocalMachine, machineName)
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
