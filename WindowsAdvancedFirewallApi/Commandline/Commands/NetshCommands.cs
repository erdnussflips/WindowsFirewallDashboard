using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Commands
{
	public abstract class NetshCommands
	{
		[DllImport("shell32.dll", EntryPoint = "IsUserAnAdmin")]
		private static extern bool HasAdministratorPrivileges();

		public enum InternalCode { OK, AdminRightsNeeded, UnsupportedParameter }

		public struct NetshResult {
			public string Command;
			public string StandardOutput;
			public string StandardError;
			public int ExitCode;
			public InternalCode Code;
		}

		private static NetshResult RunProcess(string application, string args)
		{
			var procStartInfo = new ProcessStartInfo(application, args)
			{
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};

			var process = Process.Start(procStartInfo);
			//process.WaitForExit(3000);

			var result = new NetshResult
			{
				Command = application + " " + args,
				StandardOutput = process.StandardOutput.ReadToEnd(),
				StandardError = process.StandardError.ReadToEnd(),
				ExitCode = process.ExitCode,
                Code = InternalCode.OK
			};

			process.Close();

			return result;
		}

		protected static NetshResult RunCommand(string command, string parameter = null, string context = null)
		{
			if(!HasAdministratorPrivileges())
			{
				return new NetshResult { Code = InternalCode.AdminRightsNeeded };
			}

			var contextcommand = "advfirewall ";

			if(context != null)
			{
				contextcommand += context + " ";
			}

			contextcommand += command;

			if (parameter != null)
			{
				contextcommand += " " + parameter;
			}

			return RunProcess("netsh", contextcommand);
		}
	}
}
