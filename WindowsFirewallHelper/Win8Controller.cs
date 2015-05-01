using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using NetFwTypeLib;

namespace WindowsFirewallHelper
{
    public class Win8Controller : AController
    {
	    private INetFwPolicy2 firewallPolicy;
	    public INetFwPolicy2 FirewallPolicy
	    {
		    get
		    {
			    if (firewallPolicy != null) return firewallPolicy;

			    firewallPolicy = (INetFwPolicy2) Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
			    return firewallPolicy;
		    }
	    }

		protected NET_FW_PROFILE_TYPE2_ GetCurrentProfiles()
		{
			return (NET_FW_PROFILE_TYPE2_) FirewallPolicy.CurrentProfileTypes;
		}

		private FirewallStatus GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_ type)
		{
			return FirewallPolicy.get_FirewallEnabled(type) ? FirewallStatus.Enabled : FirewallStatus.Disabled;
		}
		private Boolean SetFirewallStatusByType(NET_FW_PROFILE_TYPE2_ type, Boolean status)
        {
			try
			{
				FirewallPolicy.set_FirewallEnabled(type, status);
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
        }
		private Boolean EnableFirewallByType(NET_FW_PROFILE_TYPE2_ type)
        {
            return SetFirewallStatusByType(type, true);
        }
		private Boolean DisableFirewallByType(NET_FW_PROFILE_TYPE2_ type)
		{
			return SetFirewallStatusByType(type, false);
		}

		#region Public Networks
		/* Public Networks*/
		public Boolean IsFirewallEnabledOnPublicNetworks()
		{
			return GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC) == FirewallStatus.Enabled;
		}

		public Boolean EnableFirewallOnPublicNetworks()
		{
			return EnableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC);
		}

		public Boolean DisableFirewallOnPublicNetworks()
		{
			return DisableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC);
		}
		#endregion

		#region Domain Networks
		public Boolean IsFirewallEnabledOnDomainNetworks()
		{
			return GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN) == FirewallStatus.Enabled;
		}
		public Boolean EnableFirewallOnDomainNetworks()
		{
			return EnableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);
		}

		public Boolean DisableFirewallOnDomainNetworks()
		{
			return DisableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);
		}
		#endregion

		#region Private Networks
		public Boolean IsFirewallEnabledOnPrivateNetworks()
		{
			return GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE) == FirewallStatus.Enabled;
		}
		public Boolean EnableFirewallOnPrivateNetworks()
		{
			return EnableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE);
		}

		public Boolean DisableFirewallOnPrivateNetworks()
		{
			return DisableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE);
		}
		#endregion

		public override FirewallStatus GetFirewallStatus()
		{
			var publicNetworks = IsFirewallEnabledOnPublicNetworks();
			var domainNetworks = IsFirewallEnabledOnDomainNetworks();
			var privateNetworks = IsFirewallEnabledOnPrivateNetworks();

			if(publicNetworks && domainNetworks && privateNetworks) return FirewallStatus.Enabled;
			if (publicNetworks || domainNetworks || privateNetworks) return FirewallStatus.PartialEnabled;
			return FirewallStatus.Disabled;
		}

		public override Boolean EnableFirewall()
	    {
			return EnableFirewallOnPrivateNetworks() & EnableFirewallOnDomainNetworks() & EnableFirewallOnPublicNetworks();
	    }

		public override Boolean DisableFirewall()
		{
			return DisableFirewallOnPrivateNetworks() & DisableFirewallOnDomainNetworks() & DisableFirewallOnPublicNetworks();
		}

		/*
		 * Returns unprotected network interfaces
		 */
		public void GetVulnerableInterfaces()
		{
			FirewallPolicy.get_ExcludedInterfaces(GetCurrentProfiles());
		}
    }
}
