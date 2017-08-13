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
using NLog;
using WindowsAdvancedFirewallApi.Extensions;

namespace WindowsAdvancedFirewallApi.WindowsRegistry.Factories
{
	class FirewallRuleFactory
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

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
				var propertyName = propertyParts[0];

				if (propertyParts.Count() < 2)
				{
					LOG.Debug("Fewer properties than required.");
					continue;
				}

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
				else if (propertyName.Equals("Defer")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("Edge")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("ICMP4")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("ICMP6")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("IF")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("IFType")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("LA6")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("LPort2_10")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("LPort2_20")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("LPort2_24")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("LUAuth")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("Platform")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("Platform2")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("RPort")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("RPort2_10")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("Svc")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("TTK")) { DebuggerExtensions.Break(); }
				else if (propertyName.Equals("TTK2_22")) { DebuggerExtensions.Break(); }
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
				else if (propertyName.Equals("LPort")) {
					rule.LocalPorts = propertyValue.ToFirewallPorts();
				}
				else if (propertyName.Equals("RA4", "RA42", "RA6", "RA62"))
				{
					rule.RemoteAddresses = propertyValue.ToFirewallAddresses(rule.RemoteAddresses);
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
				else
				{
					LOG.Debug($"Property '{propertyName}' is not handled.");
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
