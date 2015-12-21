using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFirewallDashboard.Resources.Localization;

namespace WindowsFirewallDashboard.Library.Utils
{
	sealed class LocalizationTool
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		private static ResourceManager _ressourceManager;

		static LocalizationTool()
		{
		}

		public static void Initialize()
		{
			_ressourceManager = new ResourceManager(nameof(Language), Assembly.GetExecutingAssembly());
		}

		public static void UpdateLanguage(string langID)
		{
			try
			{
				//Set Language
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(langID);
				LOG.Info("Changed language to {0}", langID);
			}
			catch (Exception)
			{
				LOG.Info("Can't change language to {0}", langID);
			}
		}
		public static string Translate(string key)
		{
			return _ressourceManager.GetString(key);
		}
	}
}
