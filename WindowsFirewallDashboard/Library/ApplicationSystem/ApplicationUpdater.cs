using GitHubUpdateManger;
using GitHubUpdateManger.Library;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubUpdateManger.Model;
using System.Diagnostics;
using System.Reflection;
using NLog;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class ApplicationUpdater : IApplicationUpdater
	{
		private readonly Logger LOG = LogManager.GetCurrentClassLogger();

		/*private static ApplicationUpdater _singleton;
		protected static ApplicationUpdater Singleton
		{
			get
			{
				if (_singleton == null)
				{
					_singleton = new ApplicationUpdater();
				}

				return _singleton;
			}
		}
		public static ApplicationUpdater Instance => Singleton;*/

		public readonly GitHubUpdateManager updateManager;

		private readonly DateTime lastCheck = DateTime.Today.AddDays(-1);

		public ApplicationUpdater()
		{
			updateManager = new GitHubUpdateManager(ApplicationConstants.GitHubApplicationRepository, ApplicationConstants.GitHubApplicationUser)
			{
				ApplicationUpdater = this
			};
		}

		public async Task CheckForUpdatesAsync()
		{
			try
			{
				await updateManager.CheckForReleasesAsync(Assembly.GetExecutingAssembly().GetName().Version, true);
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}
		}

		public void UpdatesAvailable(List<RepositoryRelease> updates)
		{
		}

		public void InstallUpdate()
		{

		}
	}
}
