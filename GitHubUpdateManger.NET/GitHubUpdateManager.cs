using GitHubUpdateManger.Library;
using GitHubUpdateManger.Model;
using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdateManger
{
	public class GitHubUpdateManager
	{
		private Library.CustomOctokit.GitHubClient github;

		private string RepositoryName { get; set; }
		private string RepositoryOwnerName { get; set; }

		public List<RepositoryRelease> Updates { get; private set; }

		public IApplicationUpdater ApplicationUpdater { get; set; }

		public GitHubUpdateManager(string repositoryName, string repositoryOwnerName)
		{
			RepositoryName = repositoryName;
			RepositoryOwnerName = repositoryOwnerName;

			github = new Library.CustomOctokit.GitHubClient(new ProductHeaderValue(RepositoryName));

			Updates = new List<RepositoryRelease>();
		}

		public async Task CheckForReleasesAsync(Version currentVersion, bool allowPrereleases = false)
		{
			Updates.Clear();
			var releases = await github.Release.GetPaginatedAsync(RepositoryOwnerName, RepositoryName, 1, 10);

			foreach (var release in releases)
			{
				if (!allowPrereleases && release.Prerelease)
				{
					continue;
				}

				var assets = await github.Release.GetAllAssets(RepositoryOwnerName, RepositoryName, release.Id);

				var update = new RepositoryRelease(release, new List<ReleaseAsset>(assets));
				Updates.Add(update);
			}

			SendUpdates(currentVersion);
		}

		private void SendUpdates(Version currentVersion)
		{
			if (this.ApplicationUpdater == null)
			{
				return;
			}

			var relevantUpdates = new List<RepositoryRelease>();

			foreach (var update in this.Updates)
			{
				if (update.Version > currentVersion)
				{
					relevantUpdates.Add(update);
				}
			}

			ApplicationUpdater.UpdatesAvailable(relevantUpdates);
		}

		public async Task DownloadReleaseAssetsAsync(RepositoryRelease update)
		{
		}
	}
}
