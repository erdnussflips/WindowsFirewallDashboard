using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindowsFirewallDashboard.ViewModel
{
	public class ControlViewModel : BaseViewModel
	{
		private Grid currentInformationGrid;

		private ICommand showInformationGridCommand;
		public ICommand ShowInformationGridCommand
		{
			get
			{
				if (showInformationGridCommand == null)
				{
					showInformationGridCommand = new RelayCommand(param => ShowInformationGrid(param));
				}
				return showInformationGridCommand;
			}
		}

		private void changeCurrentInformationGrid(Grid newGrid)
		{
			if(currentInformationGrid != null)
			{
				currentInformationGrid.Visibility = Visibility.Collapsed;
			}

			currentInformationGrid = newGrid;
			currentInformationGrid.Visibility = Visibility.Visible;
		}

		private void ShowInformationGrid(object @object)
		{
			if (@object is Grid)
			{
				changeCurrentInformationGrid(@object as Grid);
			}
		}
	}
}
