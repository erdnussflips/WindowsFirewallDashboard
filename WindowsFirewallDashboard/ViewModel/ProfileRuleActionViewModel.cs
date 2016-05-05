using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Utils;
using WindowsFirewallDashboard.Library.ApplicationSystem;
using WindowsFirewallDashboard.Model;
using static WindowsFirewallDashboard.Model.ProfileRuleAction;

namespace WindowsFirewallDashboard.ViewModel
{
    class ProfileRuleActionViewModel : GenericViewModel<ProfileRuleAction>
    {
        private FirewallProfile Profile { get; set; }

        public ProfileRuleActionViewModel(ProfileRuleAction model, FirewallProfile profile) : base(model)
        {
            Profile = profile;
        }

        public Technique InboundAction
        {
            get
            {
                return Model.InboundAction;
            }
            set
            {
                SetPropertyValue(nameof(Model.InboundAction), value);

                var attribute = EnumUtils.GetAttribute<Technique, RelatedFirewallActionAttribute>(InboundAction);
                if (attribute != null)
                {
                    Profile.DefaultInboundAction = attribute.Action;
                }
            }
        }

        public Technique OutboundAction
        {
            get
            {
                return Model.OutboundAction;
            }
            set
            {
                SetPropertyValue(nameof(Model.OutboundAction), value);

                var attribute = EnumUtils.GetAttribute<Technique, RelatedFirewallActionAttribute>(OutboundAction);
                if (attribute != null)
                {
                    Profile.DefaultOutboundAction = attribute.Action;
                }
            }
        }
    }
}
