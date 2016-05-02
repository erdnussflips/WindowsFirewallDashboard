using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitHubUpdateManger.Model
{
	public class RepositoryRelease
	{
		private const string VERSION_PATTERN = @"(\d+(\.\d+){1,4})";

		private Release githubRelease { get; set; }

		private List<ReleaseAsset> githubReleaseAssets { get; set; }

		public Version Version { get; private set; }

		public DateTime PublishedAt => githubRelease.CreatedAt.LocalDateTime;

		public string Description => githubRelease.Body;

		internal RepositoryRelease(Release release, List<ReleaseAsset> assets)
		{
			if (release == null)
			{
				throw new ArgumentNullException(nameof(release));
			}

			githubRelease = release;
			githubReleaseAssets = assets;
			parseReleaseName();
		}

		private void parseReleaseName()
		{
			var name = githubRelease.Name;

			var result = Regex.Match(name, VERSION_PATTERN);

			if (result.Success)
			{
				var group = result.Groups[0];
				Version = new Version(group.Value);
			}
		}
	}
}
