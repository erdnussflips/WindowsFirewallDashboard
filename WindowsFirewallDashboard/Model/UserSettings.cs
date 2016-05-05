using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Data;
using WindowsFirewallDashboard.Library;
using WindowsFirewallDashboard.Library.ApplicationSystem;

namespace WindowsFirewallDashboard.Model
{
	[Serializable]
	class UserSettings
	{
		public bool CheckForUpdatesAutomatically { get; set; }
		public bool InstallUpdatesAutomatically { get; set; }
		public bool NotifyForNewNetworkAccess { get; set; }

		public ProfileRuleAction PrivateRuleAction { get; set; }
		public ProfileRuleAction PublicRuleAction { get; set; }
		public ProfileRuleAction DomainRuleAction { get; set; }

		public UserSettings()
		{
			CheckForUpdatesAutomatically = true;
			InstallUpdatesAutomatically = true;
			NotifyForNewNetworkAccess = true;

			PrivateRuleAction = new ProfileRuleAction(FirewallProfileType.Private);
			PublicRuleAction = new ProfileRuleAction(FirewallProfileType.Public);
			DomainRuleAction = new ProfileRuleAction(FirewallProfileType.Domain);
	}

		public void UpdateRuleActions(FirewallProfile privateProfile, FirewallProfile publicProfile, FirewallProfile domainProfile)
		{
			UpdateRuleAction(PrivateRuleAction, privateProfile);
			UpdateRuleAction(PublicRuleAction, publicProfile);
			UpdateRuleAction(DomainRuleAction, domainProfile);
		}

		private static void UpdateRuleAction(ProfileRuleAction profileRuleAction, FirewallProfile profile)
		{
			profileRuleAction.InboundAction = ProfileRuleAction.GetMatchedTechnique(profile.DefaultInboundAction);
			profileRuleAction.OutboundAction = ProfileRuleAction.GetMatchedTechnique(profile.DefaultOutboundAction);
		}
	}
}
