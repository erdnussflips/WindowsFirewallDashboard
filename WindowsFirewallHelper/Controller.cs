using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using NetFwTypeLib;

namespace WindowsFirewallHelper
{
	public enum FirewallStatus
	{
		Enabled, Disabled
	}

    public class Controller
    {
		[DllImport("shell32.dll", EntryPoint = "IsUserAnAdmin")]
		public static extern bool IsUserAnAdministrator();

		private static Object getFwObject(String typeName)
		{
			Type type;

			switch (typeName)
			{
				case "INetFwMgr":
				{
					type = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
					break;
				}
				case "INetAuthApp":
				{
					type = Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"));
					break;
				}
				case "INetOpenPort":
				{
					type = Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"));
					break;
				}
				default:
					return null;
			}

			return Activator.CreateInstance(type);
		}

	    private INetFwMgr firewallManager;

	    public INetFwMgr FirewallManager
	    {
		    get
		    {
			    if (firewallManager != null) return firewallManager;

			    //var type = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
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

	    public FirewallStatus GetFirewallStatus()
	    {
			var firewallEnabled = FirewallManager.LocalPolicy.CurrentProfile.FirewallEnabled;
		    return firewallEnabled ? FirewallStatus.Enabled : FirewallStatus.Disabled;
	    }

	    public Boolean EnableFirewall()
	    {
		    try
		    {
				firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled = true;
		    }
		    catch (Exception)
		    {
			    return false;
		    }

		    return true;
	    }

		public Boolean DisableFirewall()
		{
			try
			{
				firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled = false;
			}
			catch (Exception)
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
			application.Enabled =  true; //enable it
			application.IpVersion = ipVersion;
			application.RemoteAddresses = remoteAddresses;
			application.Scope = scope;

			/*now add this application to AuthorizedApplications collection */
			applications.Add(application);
		} 
    }
}
