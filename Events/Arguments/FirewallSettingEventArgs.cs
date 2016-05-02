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
	public class FirewallSettingEventArgs<TSetting> : FirewallExtendedOriginEventArgs<TSetting> where TSetting : FirewallSetting, new()
	{
		public TSetting Setting
		{
			get { return Data; }
			protected set { Data = value; }
		}

		internal FirewallSettingEventArgs(EventLogEntry @event) : base(@event)
		{
		}

		protected void SetSettingAttributes(int iSettingType, int iSettingValue, int iSettingValueSize, int iSettingValueDisplay)
		{
			Setting.Type = EnumUtils.ParseStringValue(FirewallLogEvent.ReplacementStrings[iSettingType], FirewallSetting.SettingType.Unkown);

			var settingValue = FirewallLogEvent.ReplacementStrings[iSettingValue];
			if (settingValue == string.Empty)
			{
				Setting.Value = FirewallSetting.ValueTypes.Common.Empty;
			}
			else
			{
				if (Setting.Type == FirewallSetting.SettingType.IncomingStandardAction
					|| Setting.Type == FirewallSetting.SettingType.OutgoingStandardAction)
				{
					Setting.Value = EnumUtils.ParseStringValue(settingValue, FirewallSetting.ValueTypes.StandardAction.Unkown);
				}
				else
				{
					Setting.Value = EnumUtils.ParseStringValue(settingValue, FirewallSetting.ValueTypes.Common.Unkown);
				}
			}

			Setting.ValueSize = FirewallLogEvent.ReplacementStrings[iSettingValueSize].ParseInteger(0);
			Setting.ValueString = FirewallLogEvent.ReplacementStrings[iSettingValueDisplay];
		}
	}

	public class FirewallSettingEventArgs : FirewallSettingEventArgs<FirewallSetting>
	{
		internal FirewallSettingEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}

		protected void SetAttributes()
		{
			SetAttributes(4, 5, 6);
			SetSettingAttributes(0, 2, 1, 3);
		}
	}
}
