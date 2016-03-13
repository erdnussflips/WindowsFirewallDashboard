using Microsoft.Win32.TaskScheduler;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class InstallManager
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		public void Install()
		{

		}

		public void Deinstall()
		{

		}

		public bool IsAutostartTaskInstalled()
		{
			using (TaskService ts = new TaskService())
			{
				try
				{
					return false;
					// Remove the task we just created
				}
				catch (Exception ex)
				{
					LOG.Error(ex);
				}
			}

			return false;
		}

		public void InstallAutostartTask()
		{
			// Get the service on the local machine
			using (TaskService ts = new TaskService())
			{
				try
				{
					// Create a new task definition and assign properties
					TaskDefinition td = ts.NewTask();
					td.RegistrationInfo.Author = ApplicationInformation.GetApplicationCompany();
					td.RegistrationInfo.Date = new DateTime();
					td.RegistrationInfo.Description = ApplicationInformation.GetApplicationName();
					td.Principal.RunLevel = TaskRunLevel.Highest;

					// Create a trigger that will fire the task at this time every other day
					td.Triggers.Add(new LogonTrigger { Enabled = true });

					// Create an action that will launch Notepad whenever the trigger fires
					td.Actions.Add(new ExecAction(ApplicationInformation.GetApplicationFileInstallPath()));

					// Register the task in the root folder
					ts.RootFolder.RegisterTaskDefinition(ApplicationInformation.GetApplicationName(), td);
				}
				catch (Exception ex)
				{
					LOG.Error(ex);
				}
			}
		}

		public void DeinstallAutostartTask()
		{
			// Get the service on the local machine
			using (TaskService ts = new TaskService())
			{
				try
				{
					// Remove the task we just created
					ts.RootFolder.DeleteTask(ApplicationInformation.GetApplicationName());
				}
				catch (Exception ex)
				{
					LOG.Error(ex);
				}
			}
		}
	}
}
