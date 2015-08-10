using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Enum.Firewall;

namespace WindowsAdvancedFirewallApi.Commandline.Commands
{
	public class GeneralCommands : NetshCommands
	{
		public static NetshResult Export(Uri path) => RunCommand("export", path.AbsolutePath);
		public static NetshResult Import(Uri path) => RunCommand("import", path.AbsolutePath);
		public static NetshResult Reset(Uri path = null)
		{
			if(path != null)
			{
				return RunCommand("reset", path.AbsolutePath);
			}

			return RunCommand("reset");
		}

		protected static NetshResult SetFirewallProperty(FirewallProfile profile, string property, string parameter)
		{
			return RunCommand("set " + profile.value + " " + property, parameter);
		}

		public static NetshResult SetFirewallState(FirewallProfile profile, FirewallState state)
		{
			return SetFirewallProperty(profile, "state", state.value);
		}

		public static NetshResult SetFirewallPolicy(FirewallProfile profile, FirewallPolicyInbound inbound, FirewallPolicyOutbound outbound)
		{
			return SetFirewallProperty(profile, "firewallpolicy", inbound.value + "," + outbound.value);
		}

		protected static NetshResult SetFirewallSetting(FirewallProfile profile, string setting, string parameter)
		{
			return SetFirewallProperty(profile, "settings", setting + " " + parameter);
		}

		public static NetshResult SetFirewallSettingLocalRules(FirewallProfile profile, FirewallSettingLocalRules setting)
		{
			return SetFirewallSetting(profile, FirewallSettingLocalRules.Name, setting.value);
		}

		public static NetshResult SetFirewallSettingLocalConsecRules(FirewallProfile profile, FirewallSettingLocalRulesConsec setting)
		{
			return SetFirewallSetting(profile, FirewallSettingLocalRulesConsec.Name, setting.value);
		}

		public static NetshResult SetFirewallSettingInboundNotification(FirewallProfile profile, FirewallSettingInboundNotification setting)
		{
			return SetFirewallSetting(profile, FirewallSettingInboundNotification.Name, setting.value);
		}

		public static NetshResult SetFirewallSettingRemoteManagement(FirewallProfile profile, FirewallSettingRemoteManagement setting)
		{
			return SetFirewallSetting(profile, FirewallSettingRemoteManagement.Name, setting.value);
		}

		public static NetshResult SetFirewallSettingUnicastResponseToMulticast(FirewallProfile profile, FirewallSettingUnicastResponseToMulticast setting)
		{
			return SetFirewallSetting(profile, FirewallSettingUnicastResponseToMulticast.Name, setting.value);
		}

		protected static NetshResult SetFirewallLogging(FirewallProfile profile, string setting, string parameter)
		{
			return SetFirewallProperty(profile, "logging", setting + " " + parameter);
		}

		public static NetshResult SetFirewallLoggingAllowedConnections(FirewallProfile profile, FirewallLoggingAllowedConnections setting)
		{
			return SetFirewallLogging(profile, FirewallLoggingAllowedConnections.Name, setting.value);
		}

		public static NetshResult SetFirewallLoggingDroppedConnections(FirewallProfile profile, FirewallLoggingDroppedConnections setting)
		{
			return SetFirewallLogging(profile, FirewallLoggingDroppedConnections.Name, setting.value);
		}

		public static NetshResult SetFirewallLoggingFilename(FirewallProfile profile, FirewallLoggingFilename setting)
		{
			if(setting.FolderNeedsPermission())
			{
				var directory = new DirectoryInfo(Path.GetDirectoryName(setting.value));
				var acl = directory.GetAccessControl();

				acl.AddAccessRule(new FileSystemAccessRule(@"NT SERVICE\mpssvc", FileSystemRights.Write, AccessControlType.Allow));
				directory.SetAccessControl(acl);
			}

			return SetFirewallLogging(profile, FirewallLoggingFilename.Name, setting.value);
		}

		public static NetshResult SetFirewallLoggingMaxFilesize(FirewallProfile profile, FirewallLoggingMaxFilesize setting)
		{
			return SetFirewallLogging(profile, FirewallLoggingMaxFilesize.Name, setting.value);
		}
	}
}
