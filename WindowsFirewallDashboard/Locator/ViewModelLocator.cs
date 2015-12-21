using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFirewallDashboard.ViewModel;

namespace WindowsFirewallDashboard.Locator
{
	class ViewModelLocator
	{
		private static ControlViewModel _controlVM;

		public static ControlViewModel ControlVM
		{
			get
			{
				if(_controlVM == null)
				{
					_controlVM = new ControlViewModel();
				}

				return _controlVM;
			}
		}

	}
}
