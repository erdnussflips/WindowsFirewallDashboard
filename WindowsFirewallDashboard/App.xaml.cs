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

		[STAThread]
		public static void Main()
		{
			if (SingleInstance<App>.InitializeAsFirstInstance(ApplicationInformation.GetApplicationName()))
			{
				new App().Run();

				// Allow single instance code to perform cleanup operations
				SingleInstance<App>.Cleanup();
			}
		}

		#region ISingleInstanceApp Members
		public bool SignalExternalCommandLineArgs(IList<string> args)
		{
			return true;
		}

		#endregion

		public App() : base()
		{
			InitializeComponent();
			DispatcherUnhandledException += App_DispatcherUnhandledException;
		}

		#region Lifecycle
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ApplicationManager.Instance.OnStart();
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
			ApplicationManager.Instance.OnActivate();
		}

		protected override void OnDeactivated(EventArgs e)
		{
			base.OnDeactivated(e);
			ApplicationManager.Instance.OnDeactivate();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			ApplicationManager.Instance.OnExit();
		}

		protected override void OnLoadCompleted(NavigationEventArgs e)
		{
			base.OnLoadCompleted(e);
		}

		protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
		{
			base.OnSessionEnding(e);
		}
		#endregion

		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			LOG.Error(e);
		}
	}
}
