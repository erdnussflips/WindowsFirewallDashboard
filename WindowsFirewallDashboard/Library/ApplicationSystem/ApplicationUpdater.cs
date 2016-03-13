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

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class ApplicationUpdater : IApplicationUpdater
	{
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

		public GitHubUpdateManager updateManager;

		private DateTime lastCheck = DateTime.Today.AddDays(-1);

		public ApplicationUpdater()
		{
			updateManager = new GitHubUpdateManager("WindowsFirewallDashboard", "ErdnussFlipS")
			{
				ApplicationUpdater = this
			};
		}

		public async void CheckForUpdates()
		{
			await updateManager.CheckForReleases(Assembly.GetExecutingAssembly().GetName().Version, true);
		}

		public void UpdatesAvailable(List<RepositoryRelease> updates)
		{
		}

		public void InstallUpdate()
		{

		}
	}
}
