using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter;
using WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallPolicy;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Logging;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Settings;

namespace WindowsAdvancedFirewallApi.Commandline.Commands
{
	public class GeneralCommands : NetshCommands
	{
		public static NetshResult Export(PathParameter parameter) => RunCommand("export", parameter.Value);
		public static NetshResult Import(PathParameter parameter) => RunCommand("import", parameter.Value);
		public static NetshResult Reset(PathParameter parameter = null)
		{
			if(parameter != null)
			{
				return RunCommand("reset", parameter.Value);
			}

			return RunCommand("reset");
		}

		public static NetshResult SetFirewallLoggingAllowedConnections(ProfileParameter profile, AllowedConnectionsParameter parameter)
		{
			return SetFirewallLogging(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallLoggingDroppedConnections(ProfileParameter profile, DroppedConnectionsParameter parameter)
		{
			return SetFirewallLogging(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallLoggingFilename(ProfileParameter profile, FilenameParameter parameter)
		{
			if (parameter.ParameterValue.FilefolderNeedsPermission())
			{
				var directory = new DirectoryInfo(Path.GetDirectoryName(parameter.ParameterValue.Value));
				var acl = directory.GetAccessControl();

				acl.AddAccessRule(new FileSystemAccessRule(@"NT SERVICE\mpssvc", FileSystemRights.Write, AccessControlType.Allow));
				directory.SetAccessControl(acl);
			}

			return SetFirewallLogging(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallLoggingMaxFilesize(ProfileParameter profile, MaxFilesizeParameter parameter)
		{
			return SetFirewallLogging(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallPolicy(ProfileParameter profile, InboundPolicyParameter parameterInbound, OutboundPolicyParameter parameterOutbound)
		{
			return SetFirewallProperty(profile, "firewallpolicy", parameterInbound.Value + "," + parameterOutbound.Value);
		}

		public static NetshResult SetFirewallSettingInboundNotification(ProfileParameter profile, InboundUserNotificationParameter parameter)
		{
			return SetFirewallSetting(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallSettingLocalConsecRules(ProfileParameter profile, LocalConsecRulesParameter parameter)
		{
			return SetFirewallSetting(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallSettingLocalRules(ProfileParameter profile, LocalFirewallRulesParameter parameter)
		{
			return SetFirewallSetting(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallSettingRemoteManagement(ProfileParameter profile, RemoteManagementParameter parameter)
		{
			return SetFirewallSetting(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallSettingUnicastResponseToMulticast(ProfileParameter profile, UnicastResponseToMulticastParameter parameter)
		{
			return SetFirewallSetting(profile, parameter.Name, parameter.ParameterValue.Value);
		}

		public static NetshResult SetFirewallState(ProfileParameter profile, StateParameter parameter)
		{
			return SetFirewallProperty(profile, "state", parameter.Value);
		}

		protected static NetshResult SetFirewallLogging(ProfileParameter profile, string parametername, string parametervalue)
		{
			return SetFirewallProperty(profile, "logging", parametername + " " + parametervalue);
		}

		private static NetshResult SetFirewallProperty(ProfileParameter profile, string property, string parameter)
		{
			return RunCommand("set " + profile.Value + " " + property, parameter);
		}
		private static NetshResult SetFirewallSetting(ProfileParameter profile, string setting, string parameter)
		{
			return SetFirewallProperty(profile, "settings", setting + " " + parameter);
		}
	}
}
