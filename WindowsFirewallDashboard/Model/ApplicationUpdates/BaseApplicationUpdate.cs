using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Model.ApplicationUpdates
{
	abstract class BaseApplicationUpdate
	{
		private const string VERSION_PATTERN = @"(\d+(\.\d+){1,4})";

		private Release githubRelease { get; set; }

		public Version Version { get; private set; }

		protected BaseApplicationUpdate(Release githubRelease)
		{
			this.githubRelease = githubRelease;
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
