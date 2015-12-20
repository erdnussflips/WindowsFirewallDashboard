using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Model.ApplicationUpdates
{
	class ReleaseApplicationUpdate : BaseApplicationUpdate
	{
		public ReleaseApplicationUpdate(Release githubRelease) : base(githubRelease)
		{
		}
	}
}
