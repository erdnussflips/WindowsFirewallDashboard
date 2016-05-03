using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.COM;
using WindowsAdvancedFirewallApi.COM.Types;
using WindowsAdvancedFirewallApi.Commandline.Commands;
using WindowsAdvancedFirewallApi.Commandline.Parameter;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Logging;
using Debugger = System.Diagnostics.Debugger;

namespace WindowsAdvancedFirewallApi
{
	public static class ApiTest
	{
		public static void run()
		{
			comTest();
		}

		public static void comTest()
		{
			FirewallCOMManager.Instance.Disable(FirewallProfile.Private);
			FirewallCOMManager.Instance.Enable(FirewallProfile.Private);
		}

		public static void netshTest()
		{
			var result = GeneralCommands.SetFirewallState(ProfileParameter.All, StateParameter.Off);
			result = GeneralCommands.SetFirewallState(ProfileParameter.All, StateParameter.Default);
			result = GeneralCommands.SetFirewallLoggingAllowedConnections(ProfileParameter.All, new AllowedConnectionsParameter { ParameterValue = AllowedConnectionsParameter.Value.Default });


			//var result = GeneralCommands.Export(new Uri(@"D:\firewallSettings.wfw"));

			//var result2 = GeneralCommands.SetFirewallPolicy(FirewallProfile.Public, FirewallPolicyInbound.BlockInboundAlways, FirewallPolicyOutbound.BlockOutbound);
			//result2 = GeneralCommands.SetFirewallPolicy(FirewallProfile.Public, FirewallPolicyInbound.Default, FirewallPolicyOutbound.Default);
		}
		public static void test()
		{
			var session = InitialSessionState.CreateDefault();
			//session.ImportPSModule(new string[] { "NetSecurity" });

			var umgebung = RunspaceFactory.CreateRunspace(session);
			umgebung.Open();

			using (PowerShell ps = PowerShell.Create())
			{
				ps.Runspace = umgebung;
				/*
					#Get-ExecutionPolicy -List;
					#Set-ExecutionPolicy Unrestricted -Scope Process;
					#Get-Module -ListAvailable;
					Get-Command -Noun '*firewall*';
				*/
				//ps.AddCommand("(Get-Module NetSecurity -ListAvailable).Path");
				//ps.AddCommand("import-module NetSecurity");
				//ps.AddCommand(@"Import-Module 'C:\WINDOWS\system32\WindowsPowerShell\v1.0\Modules\NetSecurity\NetSecurity.psd1'");
				//var result1 = ps.Invoke();

				//ps.AddScript("Get-Command -Noun '*firewall*'");
				ps.AddScript(@"Set-ExecutionPolicy Unrestricted -Scope Process; Get-ExecutionPolicy -List; Get-Module -ListAvailable | Import-Module; Get-Module -List; Get-NetFirewallProfile;");
				var result = ps.Invoke();

				var informationrecords = new List<InformationalRecord>();
				informationrecords.AddRange(ps.Streams.Debug);
				informationrecords.AddRange(ps.Streams.Warning);
				informationrecords.AddRange(ps.Streams.Verbose);
				foreach (var item in informationrecords)
				{
					Console.Out.WriteLine(item.Message);
				}

				var errorrecords = new List<ErrorRecord>(ps.Streams.Error);
				foreach (var item in errorrecords)
				{
					Console.Out.WriteLine(item.ErrorDetails);
				}

				var progressrecords = new List<ProgressRecord>(ps.Streams.Progress);
				foreach (var item in progressrecords)
				{
					Console.Out.WriteLine(item.StatusDescription);
				}

				foreach (var item in result)
				{
					if (item.BaseObject.GetType().IsAssignableFrom(typeof(PSModuleInfo)))
					{
						var module = (PSModuleInfo)item.BaseObject;
						Console.Out.WriteLine(module.Name);

						if(module.Name.Equals("NetSecurity"))
						{
							Debugger.Break();
						}
					}
				}

				Debugger.Break();
			}
		}

	}
}
