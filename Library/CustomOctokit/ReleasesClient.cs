using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdateManger.Library.CustomOctokit
{
	class ReleasesClient : Octokit.ReleasesClient
	{
		public ReleasesClient(IApiConnection con) : base(con) { }


		public Task<IReadOnlyList<Release>> GetPaginated(string owner, string name, int page, int perPage)
		{
			Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
			Ensure.ArgumentNotNullOrEmptyString(name, "repository name");

			var endpoint = ApiUrls.Releases(owner, name);
			var parameter = new Dictionary<string, string>();
			parameter.Add("page", page.ToString());
			parameter.Add("per_page", perPage.ToString());
			return ApiConnection.GetAll<Release>(endpoint, parameter, "application/vnd.github.v3");
		}
	}
}
