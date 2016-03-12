﻿using System;
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
		private static MainViewModel _main;

		public static MainViewModel Main
		{
			get
			{
				if(_main == null)
				{
					_main = new MainViewModel();
				}

				return _main;
			}
		}

	}
}
