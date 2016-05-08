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
	class RuleMatcher
	{
		private RegistryKey RulesKey { get; set; }
		private FirewallRuleFactory Factory { get; set; } = new FirewallRuleFactory();

		public RuleMatcher(RegistryKey rootKey)
		{
			RulesKey = rootKey.OpenSubKey(ApiConstants.REGISTRY_KEY_RULES);
		}

		public IDictionary<string, IList<IFirewallRule>> GetRules()
		{
			var rules = new Dictionary<string, IList<IFirewallRule>>();

			var syncLock = new object();

			Parallel.ForEach(RulesKey.GetValueNames(), valueName =>
			{
				var value = RulesKey.GetValue(valueName);

				if (value is string)
				{
					var castedValue = value as string;

					var rule = Factory.CreateFromString(valueName, castedValue);

					lock (syncLock)
					{
						var namedRules = rules.GetValue(rule.Name, new List<IFirewallRule>());
						namedRules.Add(rule);

						rules.Put(rule.Name, namedRules);
					}
				}
			});

			//foreach (var valueName in RulesKey.GetValueNames())
			//{

			//}

			return rules;
		}
	}
}
