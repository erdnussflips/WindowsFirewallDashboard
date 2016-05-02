using Microsoft.Win32.TaskScheduler;
using NLog;
using SharpShell.ServerRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFirewallCore;
using WindowsFirewallCore.Extensions;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class InstallManager
	{
		#region InstalltionStatus
		public sealed class InstallationInfoAttribute : Attribute
		{
			public bool? Value { get; private set; }

			internal InstallationInfoAttribute(bool value, bool isNull = false)
			{
				Value = isNull ? (bool?)null : value;
			}
		}

		public enum InstallationStatus
		{
			[InstallationInfo(false)] Uninstalled,
			[InstallationInfo(true)] Installed,
			[InstallationInfo(false, true)] PartiallyInstalled,
		}
		#endregion

		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();
		private static readonly string TaskName = ApplicationInformation.GetApplicationName();

		public void Install()
		{
            
		}

		public void Deinstall()
		{

		}

		public bool IsAutostartTaskInstalled()
		{
			try
			{
				using (TaskService ts = new TaskService())
				{
					var installedTask = ts.FindTask(TaskName, true);
					var installStatus = installedTask != null;

					LOG.Debug("Task check: " + nameof(installStatus) + "=" + installStatus);

					return installStatus;
				}
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return false;
		}

		public bool InstallAutostartTask()
		{
			if (IsAutostartTaskInstalled())
			{
				return true;
			}

			try
			{
				// Get the service on the local machine
				using (TaskService ts = new TaskService())
				{
					// Create a new task definition and assign properties
					var td = ts.NewTask();
					td.RegistrationInfo.Author = ApplicationInformation.GetApplicationCompany();
					td.RegistrationInfo.Date = new DateTime();
					td.RegistrationInfo.Description = ApplicationInformation.GetApplicationName();
					td.Principal.RunLevel = TaskRunLevel.Highest;

					// Create a trigger that will fire the task at this time every other day
					td.Triggers.Add(new LogonTrigger { Enabled = true });

					// Create an action that will launch Notepad whenever the trigger fires
					td.Actions.Add(new ExecAction(ApplicationInformation.GetApplicationFileInstallPath(), "-m"));

					// Register the task in the root folder
					ts.RootFolder.RegisterTaskDefinition(TaskName, td);

					LOG.Debug("Task registered");

					return true;
				}
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return false;
		}

		public bool DeinstallAutostartTask()
		{
			if (!IsAutostartTaskInstalled())
			{
				return true;
			}

			try
			{
				// Get the service on the local machine
				using (TaskService ts = new TaskService())
				{
					// Remove the task we just created
					ts.RootFolder.DeleteTask(TaskName);

					LOG.Debug("Task removed");

					return true;
				}
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return false;
		}

		private SharpShell.ISharpShellServer _shellIntegrationServer;
		private SharpShell.ISharpShellServer shellIntegrationServer
		{
			get
			{
				if (_shellIntegrationServer == null)
				{
					_shellIntegrationServer = new WindowsFirewallShellExtension.WindowsFirewallShellExtension();
				}

				return _shellIntegrationServer;
			}
		}

		public bool? IsShellIntegrationInstalled()
		{
			var status = InstallationStatus.Uninstalled;

			var os32bit = ServerRegistrationManager.GetServerRegistrationInfo(shellIntegrationServer, RegistrationType.OS32Bit);
			var os64bit = ServerRegistrationManager.GetServerRegistrationInfo(shellIntegrationServer, RegistrationType.OS64Bit);

			if (os32bit != null)
			{
				status = InstallationStatus.PartiallyInstalled;
			}

			if (os64bit != null && status == InstallationStatus.PartiallyInstalled)
			{
				status = InstallationStatus.Installed;
			}
			else if(os64bit != null)
			{
				status = InstallationStatus.PartiallyInstalled;
			}

			return status.GetAttribute<InstallationInfoAttribute>().Value;
		}

		public bool InstallShellIntegration()
		{
			try
			{
				ServerRegistrationManager.InstallServer(shellIntegrationServer, RegistrationType.OS32Bit, true);
				ServerRegistrationManager.RegisterServer(shellIntegrationServer, RegistrationType.OS32Bit);

				ServerRegistrationManager.InstallServer(shellIntegrationServer, RegistrationType.OS64Bit, true);
				ServerRegistrationManager.RegisterServer(shellIntegrationServer, RegistrationType.OS64Bit);

				return true;
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return false;
		}

		public bool DeinstallShellIntegration()
		{
			try
			{
				ServerRegistrationManager.UnregisterServer(shellIntegrationServer, RegistrationType.OS32Bit);
				ServerRegistrationManager.UninstallServer(shellIntegrationServer, RegistrationType.OS32Bit);

				ServerRegistrationManager.UnregisterServer(shellIntegrationServer, RegistrationType.OS64Bit);
				ServerRegistrationManager.UninstallServer(shellIntegrationServer, RegistrationType.OS64Bit);

				return true;
			}
			catch (Exception ex)
			{
				LOG.Error(ex);
			}

			return false;
		}
	}
}
