using GitHubUpdateManger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdateManger.Library
{
	public interface IApplicationUpdater
	{
		void UpdatesAvailable(List<RepositoryRelease> updates);
	}
}
