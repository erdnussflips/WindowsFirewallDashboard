using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WindowsFirewallDashboard.Library.ApplicationSystem;
using System.Windows.Navigation;
using System.Windows.Threading;
using NLog;
using Microsoft.Shell;

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für "App.xaml"
	/// </summary>
	public partial class App : Application, ISingleInstanceApp
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		#region Single instance
		[STAThread]
		public static void Main(string[] args)
		{
			try
			{
				if (SingleInstance<App>.InitializeAsFirstInstance(ApplicationInformation.GetApplicationName()))
				{
					var application = new App();
					if (args != null)
					{
						application.PrepareFromCommandline(args);
					}
					application.Run();

					// Allow single instance code to perform cleanup operations
					SingleInstance<App>.Cleanup();
				}
			}
			catch (Exception ex)
			{
				LOG.Error(ex, ex.Message);
			}
		}

		#region ISingleInstanceApp Members
		public bool SignalExternalCommandLineArgs(IList<string> args)
		{
			ApplicationManager.Instance.WindowManager.ShowWindows();
			return true;
		}
		#endregion
		#endregion

		public App() : base()
		{
			InitializeComponent();
			DispatcherUnhandledException += App_DispatcherUnhandledException;
		}

		private void PrepareFromCommandline(string[] args)
		{
			ApplicationManager.Instance.PrepareFromCommandline(args);
		}

		#region Lifecycle
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			LOG.Info(nameof(OnStartup));
			ApplicationManager.Instance.OnStartup();
			//var systemAccentColors = new ResourceDictionary();
			//systemAccentColors.Add("HighlightColor", SystemColors.HighlightColor);
			//systemAccentColors.Add("AccentColor", SystemColors.HighlightColor);
			//systemAccentColors.Add("AccentColor2", SystemColors.HighlightColor);
			//systemAccentColors.Add("AccentColor3", SystemColors.HighlightColor);
			//systemAccentColors.Add("AccentColor4", SystemColors.HighlightColor);
			//systemAccentColors.Add("IdealForegroundColor", SystemColors.HighlightTextColor);

			////var systemAccentBrushes = new ResourceDictionary();
			////var assembly = typeof(App).Assembly.GetName().Name;
			////LoadComponent(systemAccentBrushes, new Uri("/" + assembly + ";component/Resources/Accents/SystemAccentColor.xaml", UriKind.Relative));

			////var systemAccent = new ResourceDictionary();
			////systemAccent.MergedDictionaries.Add(systemAccentColors);
			////systemAccent.MergedDictionaries.Add(systemAccentBrushes);

			//var accent = new Accent();
			//accent.Name = "SystemAccent";
			//accent.Resources = systemAccentColors;


			//// add custom accent and theme resource dictionaries
			////ThemeManager.AddAccent("SystemAccent", new Uri("pack://application:,,,/Resources/Accents/SystemAccentColor.xaml"));

			////// get the theme from the current application
			//var theme = ThemeManager.DetectAppStyle(Current);

			////// now use the custom accent
			//ThemeManager.ChangeAppStyle(Current, accent, theme.Item1);
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			LOG.Info(nameof(OnActivated));
			ApplicationManager.Instance.OnActivated();
		}

		protected override void OnLoadCompleted(NavigationEventArgs e)
		{
			base.OnLoadCompleted(e);
			LOG.Info(nameof(OnLoadCompleted));
		}

		protected override void OnDeactivated(EventArgs e)
		{
			base.OnDeactivated(e);
			LOG.Info(nameof(OnDeactivated));
			ApplicationManager.Instance.OnDeactivated();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			LOG.Info(nameof(OnExit));
			ApplicationManager.Instance.OnExit();
		}

		protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
		{
			base.OnSessionEnding(e);
			LOG.Info(nameof(OnSessionEnding));
		}
		#endregion

		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			LOG.Error(e);
		}
	}
}
