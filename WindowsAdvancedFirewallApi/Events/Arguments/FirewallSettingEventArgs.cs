using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events.Objects;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallSettingEventArgs : FirewallEventArgs<FirewallSetting>
	{
		private static Logger LOG = LogManager.GetCurrentClassLogger();

		public FirewallSetting Setting
		{
			get { return Data; }
			protected set { Data = value; }
		}

		internal FirewallSettingEventArgs(EntryWrittenEventArgs eventArgs) : base(eventArgs)
		{
			SetAttributes();
		}

		protected void SetAttributes()
		{
			SetAttributes(0, 5, 6, 7);

			try
			{
				Setting.Type = (FirewallSetting.SettingType)int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[1]);
				Setting.ValueSize = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[2]);
				Setting.Value = int.Parse(FirewallLogEventArgs.Entry.ReplacementStrings[3]);
				Setting.ValueString = FirewallLogEventArgs.Entry.ReplacementStrings[4];
			}
			catch(Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info(string.Format("Primitive parse error: {0}", string.Join(",",FirewallLogEventArgs.Entry.ReplacementStrings)));
				LOG.Debug(ex);
			}
		}
	}
}