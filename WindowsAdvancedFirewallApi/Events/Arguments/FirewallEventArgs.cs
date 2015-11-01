using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public abstract class FirewallEventArgs : EventArgs
	{
		private EntryWrittenEventArgs _parentEventArgs;

		protected EntryWrittenEventArgs ParentEventArgs
		{
			get { return _parentEventArgs; }
			set { _parentEventArgs = value; }
		}

		private int _profiles;

		public int Profiles
		{
			get { return _profiles; }
			set { _profiles = value; }
		}

		private int _origin;

		public int Origin
		{
			get { return _origin; }
			set { _origin = value; }
		}

		private string _modifyingUser;

		public string ModifyingUser
		{
			get { return _modifyingUser; }
			set { _modifyingUser = value; }
		}

		private string _modifyingApplication;

		public string ModifyingApplication
		{
			get { return _modifyingApplication; }
			set { _modifyingApplication = value; }
		}

		internal FirewallEventArgs(EntryWrittenEventArgs eventArgs)
		{
			if(eventArgs == null)
			{
				throw new ArgumentOutOfRangeException("The given parameter " + nameof(eventArgs) + " is null.");
			}

			ParentEventArgs = eventArgs;
			initialize();
		}

		protected abstract void initialize();

		protected void SetAttributes(int iProfiles, int iOrigin, int iModifiyingUser, int iModifyingApplication)
		{
			try
			{
				Profiles = int.Parse(ParentEventArgs.Entry.ReplacementStrings[iProfiles]);
				Origin = int.Parse(ParentEventArgs.Entry.ReplacementStrings[iOrigin]);
				ModifyingUser = ParentEventArgs.Entry.ReplacementStrings[iModifiyingUser];
				ModifyingApplication = ParentEventArgs.Entry.ReplacementStrings[iModifyingApplication];
			}
			catch (Exception ex)
			{
				// TODO: log entry
			}
		}
	}
}
