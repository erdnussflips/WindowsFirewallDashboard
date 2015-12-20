using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Events.Objects;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallSettingEventArgs : FirewallDataEventArgs<FirewallSetting>
	{
		public FirewallSetting Setting
		{
			get { return Data; }
			protected set { Data = value; }
		}

		internal FirewallSettingEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}

		protected void SetAttributes()
		{
			SetAttributes(0, 5, 6, 7);

			LOG.Debug("ReplacementStrings: {0}", string.Join(",", FirewallLogEvent.ReplacementStrings));

			Setting.Type = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[1], FirewallSetting.SettingType.Unkown);
			Setting.ValueSize = FirewallLogEvent.ReplacementStrings[2].ParseInteger(0);

			if (FirewallLogEvent.ReplacementStrings[3] == string.Empty)
			{
				Setting.Value = FirewallSetting.SettingValue.Empty;
			}
			else
			{
				Setting.Value = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[3], FirewallSetting.SettingValue.Unkown);
			}

			Setting.ValueString = FirewallLogEvent.ReplacementStrings[4];
		}
	}
}