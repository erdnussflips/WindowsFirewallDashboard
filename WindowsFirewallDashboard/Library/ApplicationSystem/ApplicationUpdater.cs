using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFirewallDashboard.Model.ApplicationUpdates;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class ApplicationUpdater
	{
		private static ApplicationUpdater _singleton;
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
		public static ApplicationUpdater Instance => Singleton;

		public List<BaseApplicationUpdate> Updates { get; private set; }

		private ApplicationUpdater()
		{
			Updates = new List<BaseApplicationUpdate>();
		}

		public async void CheckForUpdates()
		{
			Updates.Clear();
			var github = new GitHubClient(new ProductHeaderValue(ApplicationInformation.GetApplicationName()));
			var releases = await github.Release.GetAll(ApplicationConstants.GitHubApplicationUser, ApplicationConstants.GitHubApplicationRepository);

			foreach (var release in releases)
			{
				var update = new ReleaseApplicationUpdate(release);
				Updates.Add(update);
			}
		}
	}
}
