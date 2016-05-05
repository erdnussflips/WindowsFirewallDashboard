using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsFirewallDashboard.Model;

namespace WindowsFirewallDashboard.ViewModel
{
    class UserSettingsViewModel : GenericViewModel<UserSettings>
    {
        public bool CheckForUpdatesAutomatically
        {
            get
            {
                return Model.CheckForUpdatesAutomatically;
            }
            set
            {
                SetPropertyValue(nameof(Model.CheckForUpdatesAutomatically), value);
            }
        }

        public bool InstallUpdatesAutomatically
        {
            get
            {
                return Model.InstallUpdatesAutomatically;
            }
            set
            {
                SetPropertyValue(nameof(Model.InstallUpdatesAutomatically), value);
            }
        }

        public bool NotifyForNewNetworkAccess
        {
            get
            {
                return Model.NotifyForNewNetworkAccess;
            }
            set
            {
                SetPropertyValue(nameof(Model.NotifyForNewNetworkAccess), value);
            }
        }

        public ProfileRuleActionViewModel PrivateRuleAction { get; private set; }

        public ProfileRuleActionViewModel PublicRuleAction { get; private set; }

        public ProfileRuleActionViewModel DomainRuleAction { get; private set; }

        public UserSettingsViewModel(UserSettings model) : base(model)
        {
            PrivateRuleAction = new ProfileRuleActionViewModel(Model.PrivateRuleAction, FirewallProfile.Private);
            PublicRuleAction = new ProfileRuleActionViewModel(Model.PublicRuleAction, FirewallProfile.Public);
            DomainRuleAction = new ProfileRuleActionViewModel(Model.DomainRuleAction, FirewallProfile.Domain);
        }
    }
}
