using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using NetFwTypeLib;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System.IO;
using WindowsFirewallLibrary.FirewallEvent;

namespace WindowsFirewallLibrary.FirewallController
{
    public class Win8Controller : AController
    {
	    private INetFwPolicy2 firewallPolicy;
	    public INetFwPolicy2 FirewallPolicy
	    {
		    get
		    {
			    if (firewallPolicy != null) return firewallPolicy;

			    firewallPolicy = (INetFwPolicy2) Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
			    return firewallPolicy;
		    }
	    }

		protected NET_FW_PROFILE_TYPE2_ GetGlobaleProfileType()
        {
            return NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_ALL;
        }
		protected NET_FW_PROFILE_TYPE2_ GetCurrentProfile()
		{
			return (NET_FW_PROFILE_TYPE2_) FirewallPolicy.CurrentProfileTypes;
		}

		private FirewallStatus GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_ type)
		{
			return FirewallPolicy.FirewallEnabled[type] ? FirewallStatus.Enabled : FirewallStatus.Disabled;
		}
		private bool SetFirewallStatusByType(NET_FW_PROFILE_TYPE2_ type, bool status)
        {
			try
			{
				FirewallPolicy.FirewallEnabled[type] = status;
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
        }
		private bool EnableFirewallByType(NET_FW_PROFILE_TYPE2_ type)
        {
            return SetFirewallStatusByType(type, true);
        }
		private bool DisableFirewallByType(NET_FW_PROFILE_TYPE2_ type)
		{
			return SetFirewallStatusByType(type, false);
		}
		public void RestoreFirewallDefaults()
		{
			FirewallPolicy.RestoreLocalFirewallDefaults();
		}

		#region Public Networks
		/* Public Networks*/
		public bool IsFirewallEnabledOnPublicNetworks()
		{
			return GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC) == FirewallStatus.Enabled;
		}

		public bool EnableFirewallOnPublicNetworks()
		{
			return EnableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC);
		}

		public bool DisableFirewallOnPublicNetworks()
		{
			return DisableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC);
		}
		#endregion

		#region Domain Networks
		public bool IsFirewallEnabledOnDomainNetworks()
		{
			return GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN) == FirewallStatus.Enabled;
		}
		public bool EnableFirewallOnDomainNetworks()
		{
			return EnableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);
		}

		public bool DisableFirewallOnDomainNetworks()
		{
			return DisableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);
		}
		#endregion

		#region Private Networks
		public bool IsFirewallEnabledOnPrivateNetworks()
		{
			return GetFirewallStatusByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE) == FirewallStatus.Enabled;
		}
		public bool EnableFirewallOnPrivateNetworks()
		{
			return EnableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE);
		}

		public bool DisableFirewallOnPrivateNetworks()
		{
			return DisableFirewallByType(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE);
		}
		#endregion

		public override FirewallStatus GetFirewallStatus()
		{
			var publicNetworks = IsFirewallEnabledOnPublicNetworks();
			var domainNetworks = IsFirewallEnabledOnDomainNetworks();
			var privateNetworks = IsFirewallEnabledOnPrivateNetworks();

			if(publicNetworks && domainNetworks && privateNetworks) return FirewallStatus.Enabled;
			if (publicNetworks || domainNetworks || privateNetworks) return FirewallStatus.PartialEnabled;
			return FirewallStatus.Disabled;
		}

		public override bool EnableFirewall()
	    {
			return EnableFirewallOnPrivateNetworks() & EnableFirewallOnDomainNetworks() & EnableFirewallOnPublicNetworks();
	    }

		public override bool DisableFirewall()
		{
			return DisableFirewallOnPrivateNetworks() & DisableFirewallOnDomainNetworks() & DisableFirewallOnPublicNetworks();
		}

		/* Returns unprotected network interfaces
		 *
		 */
		public void GetVulnerableInterfaces()
		{
			Object[] excludedInterfacesPublic = FirewallPolicy.get_ExcludedInterfaces(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC);
			Object[] excludedInterfacesDomain = FirewallPolicy.get_ExcludedInterfaces(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN);
			Object[] excludedInterfacesPrivate = FirewallPolicy.get_ExcludedInterfaces(NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE);
		}

		private const string netshFirewallCommand = "advfirewall set allprofiles logging ";
	    private const string netshFirewallCommandArgsDroppedConnections = "droppedconnections ";
	    private const string netshFirewallCommandArgsAllowedConnections = "allowedconnections ";

		private string RemoveEmptyLines(string lines)
		{
			return Regex.Replace(lines, @"^\s*$\n|\r", "", RegexOptions.Multiline).TrimEnd();
		}

		private int RunProcess(string application, string args)
        {
			var procStartInfo = new ProcessStartInfo(application, args)
			{
				RedirectStandardOutput = true,
				RedirectStandardError = true,
	            UseShellExecute = false,
	            CreateNoWindow = true
            };

			Console.WriteLine("RunProcess command: " + application + " " + args);

			var process = Process.Start(procStartInfo);
			//process.WaitForExit(3000);
			var output = process.StandardOutput.ReadToEnd();
			var error = process.StandardError.ReadToEnd();

			Console.WriteLine("RunProcess output: " + RemoveEmptyLines(output));
			Console.WriteLine("RunProcess error: " + RemoveEmptyLines(error));

			var exitCode = process.ExitCode;

			process.Close();

			return exitCode;
        }
		public bool EnableFirewallEventLogging()
		{
			var args_filename = "filename %systemroot%\\system32\\LogFiles\\Firewall\\pfirewall.log";
			// defined in kilobytes
			var args_max_filesize = "maxfilesize 4096";

			// sets firewall logging file
			var exitCodeFilename = RunProcess("netsh", netshFirewallCommand + args_filename);
			// sets firewall max logging file size
			var exitCodeFilesize = RunProcess("netsh", netshFirewallCommand + args_max_filesize);
			// enable event logging for dropped connections
			var exitCodeDoppedConnections = RunProcess("netsh", netshFirewallCommand + netshFirewallCommandArgsDroppedConnections + "enable");
			// enable event logging for allowed connections
			var exitCodeAllowedConections = RunProcess("netsh", netshFirewallCommand + netshFirewallCommandArgsAllowedConnections + "enable");

			AddFirewallEventTask();

			return exitCodeFilename == 0 && exitCodeFilesize == 0 && exitCodeDoppedConnections == 0 && exitCodeAllowedConections == 0;
        }

		public bool DisableFirewallEventLogging()
        {
			// disable event logging for dropped connections (default configuration)
			var exitCodeDoppedConnections = RunProcess("netsh", netshFirewallCommand + netshFirewallCommandArgsDroppedConnections + "disable");
			// disable event logging for allowed connections (default configuration)
			var exitCodeAllowedConnections = RunProcess("netsh", netshFirewallCommand + netshFirewallCommandArgsAllowedConnections + "disable");

			RemoveAllFirewallEventTasks();

			return exitCodeDoppedConnections == 0 && exitCodeAllowedConnections == 0;
        }

		public void GenerateValueQueries<T>(EventTrigger eventTrigger)
		{
			var type = typeof(T);
		}

		const string TaskFolder = "WindowsFirewallDashboard";

		public void AddFirewallEventTask()
		{
			using (var ts = new TaskService())
			{
				var td = ts.NewTask();

				td.RegistrationInfo.Description = "Runs when Firewall events raised.";
				td.RegistrationInfo.Author = "Philipp S. aka ErdnussFlipS";
				td.RegistrationInfo.Date = DateTime.Now;

				using (var eventtrigger = new EventTrigger("Microsoft-Windows-Windows Firewall With Advanced Security/Firewall", null, null)
					{
						Enabled = true,
						StartBoundary = td.RegistrationInfo.Date
					})
				{
					eventtrigger.ValueQueries.Add("EventID", "Event/System/EventID");
					//eventtrigger.ValueQueries.Add("EventContent", "Event/EventData/Data[@Name='']");

					td.Triggers.Add(eventtrigger);
				}

				td.Principal.RunLevel = TaskRunLevel.Highest;
				td.Principal.LogonType = TaskLogonType.InteractiveToken;
				td.Settings.MultipleInstances = TaskInstancesPolicy.Queue;
				td.Settings.DisallowStartIfOnBatteries = false;
				td.Settings.StopIfGoingOnBatteries = false;
				td.Settings.AllowHardTerminate = true;
				td.Settings.StartWhenAvailable = false;
				td.Settings.RunOnlyIfNetworkAvailable = false;
				td.Settings.IdleSettings.StopOnIdleEnd = true;
				td.Settings.IdleSettings.RestartOnIdle = false;
				td.Settings.AllowDemandStart = false;
				td.Settings.Enabled = true;
				td.Settings.Hidden = false;
				td.Settings.RunOnlyIfIdle = false;
				td.Settings.WakeToRun = false;
				//td.Settings.ExecutionTimeLimit = ;
				td.Settings.Priority = ProcessPriorityClass.High;

				var dir = Directory.GetCurrentDirectory();
				Console.WriteLine("Current wdir: " + dir);
				using (var action = new ExecAction("WindowsFirewallEventListener.exe", "$(EventID)", dir))
				{
					td.Actions.Add(action);
				}

				try
				{
					var taskAdded = false;
					foreach (var folder in ts.RootFolder.SubFolders)
					{
						if (folder.Name == TaskFolder)
						{
							folder.RegisterTaskDefinition("Windows Firewall Event", td);
							taskAdded = true;
						}
					}
					if (!taskAdded)
					{
						var folder = ts.RootFolder.CreateFolder(TaskFolder);
						folder.RegisterTaskDefinition("Windows Firewall Event", td);
					}
				}
				catch (Exception ex)
				{
					Debugger.Break();
				}
			}
		}

		public void RemoveAllFirewallEventTasks()
		{
			using (var ts = new TaskService())
			{
				try
				{
					foreach (var folder in ts.RootFolder.SubFolders)
					{
						if (folder.Name == TaskFolder)
						{
							foreach (var task in folder.Tasks)
							{
								folder.DeleteTask(task.Name);
							}

							ts.RootFolder.DeleteFolder(folder.Name);
						}
					}
				}
				catch (Exception ex)
				{
					Debugger.Break();
				}
			}
		}
    }
}
