using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.WindowsRegistry.Types;
using WindowsAdvancedFirewallApi.Data.Interfaces;
using System.Diagnostics;
using WindowsAdvancedFirewallApi.Utils;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Resources;

namespace WindowsAdvancedFirewallApi.WindowsRegistry.Factories
{
	class FirewallRuleFactory
	{
		public IFirewallRule CreateFromString(string ruleId, string ruleString)
		{
			var rule = new FirewallRule
			{
				Id = ruleId
			};

			Parse(ruleString, rule);

			return rule;
		}

		private void Parse(string ruleString, FirewallRule rule)
		{
			var properties = ruleString.Split('|').ToList();

			var versionRegEx = $@"v({RegExTool.FLOATING_POINT_NUMBER})";
			var versionMatch = properties[0].Match(versionRegEx);

			var version = versionMatch.Success ? versionMatch.Groups[0].Value : null;
			rule.Version = version?.ParseFloat(null);
			properties.RemoveAt(0);

			foreach (var property in properties)
			{
				var propertyParts = property.Split('=');

				if (propertyParts.Count() < 2)
				{
					continue;
				}

				var propertyName = propertyParts[0];
				var propertyValue = propertyParts[1];

				#region Passing
				if (propertyName.Equals("App"))
				{
					rule.ApplicationPath = propertyValue;
				}
				else if (propertyName.Equals("LUOwn"))
				{
					rule.LocalUserOwner = propertyValue;
				}
				else if (propertyName.Equals("AppPkgId"))
				{
					rule.LocalAppPackageId = propertyValue;
				}
				#endregion
				#region Converting
				else if (propertyName.Equals("Action"))
				{
					rule.Action = propertyValue.ToFirewallAction();
				}
				else if (propertyName.Equals("Active"))
				{
					rule.Enabled = propertyValue.ToBoolean();
				}
				else if (propertyName.Equals("Dir"))
				{
					rule.Direction = propertyValue.ToFirewallDirection();
				}
				else if (propertyName.Equals("Profile"))
				{
					rule.Profiles.Add(propertyValue.ToFirewallProfileType());
				}
				else if (propertyName.Equals("Protocol"))
				{
					var protocolNumber = propertyValue.ParseInteger(null);

					if (protocolNumber != null)
					{
						rule.Protocol = FirewallProtocolUtil.ToFirewallProtocol((short)protocolNumber);
					}
				}
				#endregion
				#region Localization
				else if (propertyName.Equals("Name"))
				{
					rule.Name = GetFirewallResourceString(propertyValue);
				}
				else if (propertyName.Equals("Desc"))
				{
					rule.Description = GetFirewallResourceString(propertyValue);
				}
				else if(propertyName.Equals("EmbedCtxt"))
				{
					rule.EmbeddedContext = GetFirewallResourceString(propertyValue);
				}
				#endregion
			}

			if (rule.Profiles.Count == 0)
			{
				rule.Profiles.Add(FirewallProfileType.Public, FirewallProfileType.Private, FirewallProfileType.Domain);
			}
		}

		private string GetFirewallResourceString(string identifier)
		{
			var matchFirewallAPI = RegExTool.Match(identifier, @"@FirewallAPI.dll,-(\d+)");
			var matchICSvc = RegExTool.Match(identifier, @"@icsvc.dll,-(\d+)");

			if (matchFirewallAPI.Success)
			{
				var id = (uint)matchFirewallAPI.Groups[1].Value.ParseInteger();
				return SystemLocalizations.Instance.StringsFirewallAPI[id];
			}

			if (matchICSvc.Success)
			{
				var id = (uint)matchICSvc.Groups[1].Value.ParseInteger();
				return SystemLocalizations.Instance.StringsICSvc[id];
			}

			return identifier;
		}
	}
}
