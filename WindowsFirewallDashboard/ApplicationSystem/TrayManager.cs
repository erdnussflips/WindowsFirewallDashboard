using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WindowsFirewallDashboard.ApplicationSystem
{
	public class TrayManager
	{
		private Window _rootWindow;
		public Window RootWindow
		{
			get
			{
				return _rootWindow;
			}
			set
			{
				if(_rootWindow != value && _rootWindow != null)
				{
					_rootWindow.StateChanged -= WindowStateChanged;
				}

				if (value != null)
				{
					_rootWindow = value;
					_rootWindow.StateChanged += WindowStateChanged;
					Text = _rootWindow.Title;
				}
			}
		}
		private List<Window> ManagedWindows { get; set; }

		private NotifyIcon notifyIcon;

		public Icon Icon
		{
			get
			{
				return notifyIcon.Icon;
			}
			set
			{
				notifyIcon.Icon = value;
			}
		}

		public string Text
		{
			get
			{
				return notifyIcon.Text;
			}
			set
			{
				notifyIcon.Text = value;
			}
		}

		public event MouseEventHandler MouseClick
		{
			add
			{
				notifyIcon.MouseClick += value;
			}
			remove
			{
				notifyIcon.MouseClick -= value;
			}
		}

		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				notifyIcon.MouseDoubleClick += value;
			}
			remove
			{
				notifyIcon.MouseDoubleClick -= value;
			}
		}

		public TrayManager()
		{
			ManagedWindows = new List<Window>();
			notifyIcon = new NotifyIcon();

			MouseDoubleClick += delegate (object sender, MouseEventArgs e)
			{
				if(RootWindow != null)
				{
					RootWindow.WindowState = WindowState.Normal;
				}
			};
		}

		public void AddManagedWindows(params Window[] windows)
		{
			ManagedWindows.AddRange(windows);
		}

		public void RemoveManagedWindows(params Window[] windows)
		{
			foreach (var item in windows)
			{
				ManagedWindows.Remove(item);
			}
		}

		public void ShowManagedWindows()
		{
			foreach (var item in ManagedWindows)
			{
				item.ShowInTaskbar = true;
				item.Show();
			}
		}
		public void HideManagedWindows()
		{
			foreach (var item in ManagedWindows)
			{
				item.ShowInTaskbar = false;
				item.Hide();
			}
		}

		private void WindowStateChanged(object sender, EventArgs args)
		{
			if (RootWindow.WindowState == WindowState.Minimized)
			{
				notifyIcon.Visible = true;
				RootWindow.ShowInTaskbar = false;
			}
			else if(RootWindow.WindowState == WindowState.Normal)
			{
				notifyIcon.Visible = false;
				RootWindow.ShowInTaskbar = true;
			}
		}
	}
}
