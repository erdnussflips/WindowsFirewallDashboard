using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.ViewModel
{
	[Serializable]
	public abstract class BaseViewModel : IViewModel
	{
		protected BaseViewModel()
		{
			var initializationTask = new Task(() => Initialize());
			initializationTask.ContinueWith(result => InitializationCompletedCallback(result));
			initializationTask.Start();
		}

		protected virtual void Initialize()
		{
		}

		private void InitializationCompletedCallback(IAsyncResult result)
		{
			var initializationCompleted = InitializationCompleted;
			if (initializationCompleted != null)
			{
				var handler = InitializationCompleted;
				if (handler != null)
				{
					handler(this, new AsyncCompletedEventArgs(null, !result.IsCompleted, result.AsyncState));
				}
			}
			InitializationCompleted = null;
		}

		public event AsyncCompletedEventHandler InitializationCompleted;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
