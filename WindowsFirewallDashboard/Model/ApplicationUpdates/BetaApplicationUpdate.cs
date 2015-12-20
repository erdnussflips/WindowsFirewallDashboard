using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Model.ApplicationUpdates
{
	class BetaApplicationUpdate : BaseApplicationUpdate
	{
		public BetaApplicationUpdate(Release githubRelease) : base(githubRelease)
		{
		}
	}
}
