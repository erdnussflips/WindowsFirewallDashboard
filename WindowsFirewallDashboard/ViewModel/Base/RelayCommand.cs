using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowsFirewallDashboard.ViewModel.Base
{
	[Serializable]
	public class RelayCommand : ICommand
	{
		#region Fields
		private readonly Action<object> execute;
		private readonly Predicate<object> canExecute;
		#endregion Fields

		#region Constructors
		public RelayCommand(Action<object> execute) : this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException(nameof(execute));
			}

			this.execute = execute;
			this.canExecute = canExecute;
		}
		#endregion Constructors

		#region ICommand Members
		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			if (canExecute == null)
			{
				return true;
			}

			return canExecute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			execute(parameter);
		}
		#endregion ICommand Members
	}
}
