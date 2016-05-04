using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;

namespace WindowsFirewallDashboard.Model
{
   public class ProfileRuleAction
    {
        sealed class RelatedFirewallActionAttribute : Attribute
        {
            public FirewallAction Action { get; private set; }

            internal RelatedFirewallActionAttribute(FirewallAction action)
            {
                Action = action;
            }
        }

        public enum Technique
        {
            [RelatedFirewallAction(FirewallAction.Allow)] Allow,
            [RelatedFirewallAction(FirewallAction.Block)] Block,
            [RelatedFirewallAction(FirewallAction.Block)] BlockAndPrompt,
            [RelatedFirewallAction(FirewallAction.Block)] AutomaticOrPrompt
        }

        public FirewallProfileType Profile { get; }
        public Technique InboundAction { get; set; }
        public Technique OutboundAction { get; set; }

        public ProfileRuleAction(FirewallProfileType profile)
        {
            Profile = profile;
            InboundAction = Technique.BlockAndPrompt;
            OutboundAction = Technique.BlockAndPrompt;
        }

        public static Technique GetMatchedTechnique(FirewallAction action)
        {
            if (action == FirewallAction.Allow)
            {
                return Technique.Allow;
            }

            return Technique.Block;
        }
    }
}
