using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallSettingsEventArgs : FirewallEventArgs
	{
		private int _settingType;

		public int SettingType
		{
			get { return _settingType; }
			set { _settingType = value; }
		}

		private int _settingValueSize;

		public int SettingValueSize
		{
			get { return _settingValueSize; }
			set { _settingValueSize = value; }
		}

		private int _settingValue;

		public int SettingValue
		{
			get { return _settingValue; }
			set { _settingValue = value; }
		}

		private string _settingValueString;

		public string SettingValueString
		{
			get { return _settingValueString; }
			set { _settingValueString = value; }
		}

		internal FirewallSettingsEventArgs(EntryWrittenEventArgs eventArgs) : base(eventArgs)
		{

		}

		protected override void initialize()
		{

		}
	}
}
