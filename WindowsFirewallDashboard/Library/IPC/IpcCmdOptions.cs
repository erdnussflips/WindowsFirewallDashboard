using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Library.IPC
{
	class IpcCmdOptions
	{
		[Option('m', "minimized", Required = false, HelpText = "Start application minimized.")]
		public bool StartMinimized { get; set; }
	}
}
