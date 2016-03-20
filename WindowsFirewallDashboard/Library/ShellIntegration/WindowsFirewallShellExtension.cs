using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFirewallCore;
using WindowsFirewallCore.IPCommunication.ShellIntegration;
using WindowsFirewallCore.IPCommunication.ShellIntegration.Interfaces;
using WindowsFirewallDashboard.Library.ApplicationSystem;

namespace WindowsFirewallDashboard
{
	[ComVisible(true)]
	[COMServerAssociation(AssociationType.ClassOfExtension, ".exe")]
	public class WindowsFirewallShellExtension : SharpContextMenu
	{
		protected override bool CanShowMenu()
		{
			return true;
		}

		protected override ContextMenuStrip CreateMenu()
		{
			try
			{
				var menu = new ContextMenuStrip();

				var itemFirewallDashboard = new ToolStripMenuItem
				{
					Text = "Windows Firewall Dashboard",
				};

				var itemAddApplicationRule = new ToolStripMenuItem
				{
					Text = "Add application rule"
				};
				itemAddApplicationRule.Click += AddApplicationRule;
				itemFirewallDashboard.DropDownItems.Add(itemAddApplicationRule);

				//var itemRemoveApplicationRule = new ToolStripMenuItem();
				//var itemStatusApplicationRule = new ToolStripMenuItem();

				return menu;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			return null;
		}

		private void CloseChannel(ICommunicationObject channel)
		{
			try
			{
				channel.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				channel.Abort();
			}
		}

		private void AddApplicationRule(object sender, EventArgs e)
		{
			using (var factory = new ChannelFactory<IShellService>(new NetNamedPipeBinding(), new EndpointAddress(CoreConstants.PipeEndpoint)))
			{
				var shellService = factory.CreateChannel();

				try
				{
					shellService.AddApplicationRule(SelectedItemPaths.ElementAt(0));
					MessageBox.Show("Rule added");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					throw;
				}
				finally
				{
					CloseChannel((ICommunicationObject)shellService);
				}
			}
		}
	}
}
