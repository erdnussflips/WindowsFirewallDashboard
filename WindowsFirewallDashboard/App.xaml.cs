using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WindowsFirewallDashboard.ApplicationSystem;

namespace WindowsFirewallDashboard
{
	/// <summary>
	/// Interaktionslogik für "App.xaml"
	/// </summary>
	public partial class App : Application
	{
		public App() : base()
		{
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ApplicationManager.Instance.Start();
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
			ApplicationManager.Instance.Activate();
		}

		protected override void OnDeactivated(EventArgs e)
		{
			base.OnDeactivated(e);
			ApplicationManager.Instance.Deactivate();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			ApplicationManager.Instance.Exit();
		}
	}
}
