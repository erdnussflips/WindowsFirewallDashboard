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
	public class FirewallSettingEventArgs : FirewallEventArgs<FirewallSetting>
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
			SetAttributes(4, 5, 6);
			SetSettingAttributes(0, 1, 2, 3);
		}

		protected void SetSettingAttributes(int iSettingType, int iSettingValue, int iSettingValueSize, int iSettingValueDisplay)
		{
			Setting.Type = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[iSettingType], FirewallSetting.SettingType.Unkown);
			Setting.ValueSize = FirewallLogEvent.ReplacementStrings[iSettingValueSize].ParseInteger(0);

			var settingValue = FirewallLogEvent.ReplacementStrings[iSettingValue];
			if (settingValue == string.Empty)
			{
				Setting.Value = FirewallSetting.SettingValue.Empty;
			}
			else
			{
				Setting.Value = EnumUtils.ParseStringValue(settingValue, FirewallSetting.SettingValue.Unkown);
			}

			Setting.ValueString = FirewallLogEvent.ReplacementStrings[iSettingValueDisplay];
		}
	}
}
