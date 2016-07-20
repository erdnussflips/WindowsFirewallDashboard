using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdateManger.Model
{
	public class DownloadedReleaseAsset
	{
		private ReleaseAsset UnderlyingAsset { get; set; }

		public string DownloadLocation { get; private set; }

		public DownloadedReleaseAsset(ReleaseAsset underlyingAsset, string downloadLocation)
		{
			UnderlyingAsset = underlyingAsset;
			DownloadLocation = downloadLocation;
		}
	}
}
