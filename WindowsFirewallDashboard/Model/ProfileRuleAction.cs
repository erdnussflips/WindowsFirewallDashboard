using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Utils;
using WindowsFirewallDashboard.Library;

namespace WindowsFirewallDashboard.Model
{
   public class ProfileRuleAction
    {
        public sealed class RelatedFirewallActionAttribute : Attribute
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

        public Technique InboundAction { get; set; } = Technique.BlockAndPrompt;
        public Technique OutboundAction { get; set; } = Technique.BlockAndPrompt;

        public ProfileRuleAction(FirewallProfileType profile)
        {
            Profile = profile;
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
