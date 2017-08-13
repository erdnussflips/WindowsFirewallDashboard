using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using WindowsAdvancedFirewallApi.Utils;
using WindowsAdvancedFirewallApi.WindowsRegistry.Factories;

namespace WindowsAdvancedFirewallApi.WindowsRegistry
{
	class RuleManager
	{
		private RegistryKey FirewallRules { get; set; }
		private RegistryKey RestrictedServices_Static { get; set; }
		private RegistryKey RestrictedServices_Configurable { get; set; }
		private FirewallRuleFactory RuleFactory { get; set; } = new FirewallRuleFactory();

		public RuleManager(RegistryKey rootKey)
		{
			FirewallRules = rootKey.OpenSubKey(ApiConstants.REGISTRY_KEY_RULES);
			RestrictedServices_Static = rootKey.OpenSubKey(ApiConstants.REGISTRY_KEY_RULES_STATIC);
			RestrictedServices_Configurable = rootKey.OpenSubKey(ApiConstants.REGISTRY_KEY_RULES_CONFIGURABLE);
		}

		private IDictionary<string, IList<IFirewallRule>> LoadRulesFromRegistry(RegistryKey registryKey)
		{
			var rules = new Dictionary<string, IList<IFirewallRule>>();

			var syncLock = new object();

			Parallel.ForEach(registryKey.GetValueNames(), valueName =>
			{
				var value = registryKey.GetValue(valueName);

				if (value is string)
				{
					var castedValue = value as string;

					var rule = RuleFactory.CreateFromString(valueName, castedValue);

					lock (syncLock)
					{
						var namedRules = rules.GetValue(rule.Name, new List<IFirewallRule>());
						namedRules.Add(rule);

						rules.Put(rule.Name, namedRules);
					}
				}
			});

			return rules;
		}

		public IDictionary<string, IList<IFirewallRule>> GetRules()
		{
			return LoadRulesFromRegistry(FirewallRules);
		}

		public IDictionary<string, IList<IFirewallRule>> GetRestricedServicesStaticRules()
		{
			return LoadRulesFromRegistry(RestrictedServices_Static);
		}

		public IDictionary<string, IList<IFirewallRule>> GetRestricedServicesConfigurableRules()
		{
			return LoadRulesFromRegistry(RestrictedServices_Configurable);
		}
	}
}
