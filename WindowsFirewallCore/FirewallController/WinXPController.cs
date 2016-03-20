using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace WindowsFirewallCore.FirewallController
{
	public class WinXPController : AController
	{
		private INetFwMgr firewallManager;

		public INetFwMgr FirewallManager
		{
			get
			{
				if (firewallManager != null) return firewallManager;
				firewallManager = (INetFwMgr)getFwObject("INetFwMgr");
				return firewallManager;
			}
		}

		protected INetFwOpenPort GetNewOpenPortInstance()
		{
			return (INetFwOpenPort)getFwObject("INetOpenPort");
		}

		protected INetFwAuthorizedApplication GetNewAuthorizedApplicationInstance()
		{
			return (INetFwAuthorizedApplication)getFwObject("INetAuthApp");
		}

		public override FirewallStatus GetFirewallStatus()
		{
			var firewallEnabled = FirewallManager.LocalPolicy.CurrentProfile.FirewallEnabled;
			return firewallEnabled ? FirewallStatus.Enabled : FirewallStatus.Disabled;
		}

		public override bool EnableFirewall()
		{
			try
			{
				firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled = true;
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}

		public override bool DisableFirewall()
		{
			try
			{
				firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled = false;
			}
			catch (Exception e)
			{
				return false;
			}

			return true;
		}

		public List<INetFwOpenPort> GetCurrentGlobalAuthorizedOpenPorts()
		{
			var openPorts = new List<INetFwOpenPort>();
			var ports = FirewallManager.LocalPolicy.CurrentProfile.GloballyOpenPorts;
			var enumerate = ports.GetEnumerator();

			while (enumerate.MoveNext())
			{
				openPorts.Add((INetFwOpenPort)enumerate.Current);
			}

			return openPorts;
		}

		public List<INetFwAuthorizedApplication> GetCurrentGlobalAuthorizedApplications()
		{
			var authorizedApplications = new List<INetFwAuthorizedApplication>();
			var applications = FirewallManager.LocalPolicy.CurrentProfile.AuthorizedApplications;
			var enumerate = applications.GetEnumerator();

			while (enumerate.MoveNext())
			{
				authorizedApplications.Add((INetFwAuthorizedApplication)enumerate.Current);
			}

			return authorizedApplications;
		}

		public void AddAuthorizedPort(
			string applicationName,
			int portnumber,
			NET_FW_IP_VERSION_ ipVersion = NET_FW_IP_VERSION_.NET_FW_IP_VERSION_ANY,
			NET_FW_IP_PROTOCOL_ protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY,
			string remoteAddresses = null,
			NET_FW_SCOPE_ scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL
		)
		{
			var ports = FirewallManager.LocalPolicy.CurrentProfile.GloballyOpenPorts;

			var port = GetNewOpenPortInstance();
			port.Name = applicationName; /*name of the application using the port */
			port.Port = portnumber; /* port no */
			port.Enabled = true; /* enable the port */
			port.IpVersion = ipVersion;
			port.Protocol = protocol;
			port.RemoteAddresses = remoteAddresses;
			port.Scope = scope;

			ports.Add(port);
		}

		public void AddAuthorizedApplication(
			string applicationName,
			string applicationProcessFileName,
			NET_FW_IP_VERSION_ ipVersion = NET_FW_IP_VERSION_.NET_FW_IP_VERSION_ANY,
			string remoteAddresses = null,
			NET_FW_SCOPE_ scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL
		)
		{
			var applications = (INetFwAuthorizedApplications)FirewallManager.LocalPolicy.CurrentProfile.AuthorizedApplications;

			var application = GetNewAuthorizedApplicationInstance();
			application.Name = applicationName; /*set the name of the application */
			application.ProcessImageFileName = applicationProcessFileName; /* set this property to the location of   the executable file of the application*/
			application.Enabled = true; //enable it
			application.IpVersion = ipVersion;
			application.RemoteAddresses = remoteAddresses;
			application.Scope = scope;

			/*now add this application to AuthorizedApplications collection */
			applications.Add(application);
		}
	}
}
