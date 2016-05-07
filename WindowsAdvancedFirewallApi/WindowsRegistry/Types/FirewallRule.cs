using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Library;

namespace WindowsAdvancedFirewallApi.WindowsRegistry.Types
{
    public class FirewallRule : IFirewallRule
    {
        public double Version { get; set; }

        #region IFirewallRule
        public FirewallAction Action { get; set; }

        public bool Active { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationPath { get; set; }

        public string Description { get; set; }

        public FirewallDirection Direction { get; set; }

        public FirewallEdgeTraversal EdgeTraversal { get; set; }

        public string EmbeddedContext { get; set; }

        public bool Enabled { get; set; }

        public int Flags { get; set; }

        public string Grouping { get; set; }

        public string IcmpTypesAndCodes { get; set; }

        public string Id { get; set; }

        public dynamic Interfaces { get; set; }

        public string InterfaceTypes { get; set; }

        public FirewallAddresses LocalAddresses { get; set; }

        public string LocalAppPackageId { get; set; }

        public int LocalOnlyMapped { get; set; }

        public FirewallPorts LocalPorts { get; set; }

        public string LocalUserAuthorizedList { get; set; }

        public string LocalUserOwner { get; set; }

        public int LooseSourceMapped { get; set; }

        public string Name { get; set; }

        public IComparableList<FirewallProfileType> Profiles { get; set; }

        public FirewallProtocol Protocol { get; set; }

        public string RemoteAddresses { get; set; }

        public string RemoteMachineAuthorizationList { get; set; }

        public FirewallPorts RemotePorts { get; set; }

        public string RemoteUserAuthorizationList { get; set; }

        public int SchemaVersion { get; set; }

        public int SecurityOptions { get; set; }

        public string ServiceName { get; set; }

        public long Status { get; set; }
        #endregion
    }
}