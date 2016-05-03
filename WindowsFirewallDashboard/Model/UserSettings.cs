using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Data;
using WindowsFirewallDashboard.Library.ApplicationSystem;

namespace WindowsFirewallDashboard.Model
{
	[Serializable]
	class UserSettings
	{
		public bool CheckForUpdatesAutomatically = true;
		public bool InstallUpdatesAutomatically = true;
		public bool NotifyForNewNetworkAccess = true;

		public ProfileRuleAction PrivateRuleAction = new ProfileRuleAction(FirewallProfileType.Private);
		public ProfileRuleAction PublicRuleAction = new ProfileRuleAction(FirewallProfileType.Public);
		public ProfileRuleAction DomainRuleAction = new ProfileRuleAction(FirewallProfileType.Domain);

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
