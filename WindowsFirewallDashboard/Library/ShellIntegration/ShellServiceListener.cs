using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WindowsFirewallCore.IPCommunication.Interfaces;

namespace WindowsFirewallDashboard.Library.ShellIntegration
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class ShellServiceListener : IShellService
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();
		public void GetApplicationStatus(string applicationFilePath)
		{
			LOG.Info(applicationFilePath);
		}

		public void AddApplicationRule(IEnumerable<string> applicationFilePaths)
		{
			LOG.Info(applicationFilePaths);
		}

		public void RemoveApplicationRule(IEnumerable<string> applicationFilePaths)
		{
			LOG.Info(applicationFilePaths);
		}
	}
}
