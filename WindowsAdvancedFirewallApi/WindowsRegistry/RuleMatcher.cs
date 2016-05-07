using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.WindowsRegistry.Factories;

namespace WindowsAdvancedFirewallApi.WindowsRegistry
{
    class RuleMatcher
    {
        private RegistryKey RulesKey { get; set; }
        private FirewallRuleFactory Factory { get; set; } = new FirewallRuleFactory();

        public RuleMatcher(RegistryKey rootKey)
        {
            RulesKey = rootKey.OpenSubKey(ApiConstants.REGISTRY_KEY_RULES);
        }

        public IList<IFirewallRule> GetRules()
        {
            var rules = new List<IFirewallRule>();
            var ruleValues = new List<KeyValuePair<string, string>>();

            foreach (var valueName in RulesKey.GetValueNames())
            {
                var value = RulesKey.GetValue(valueName);

                if (value is string)
                {
                    var castedValue = value as string;

                    ruleValues.Add(new KeyValuePair<string, string>(valueName, castedValue));

                    var rule = Factory.CreateFromString(valueName, castedValue);
                    rules.Add(rule);
                }
            }

            return rules;
        }
    }
}
