using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallSetting : FirewallObject
	{
		public enum SettingType
		{
			Unkown = -1,
			WindowsFirewallActivating = 1,
			DeactivatedInterfaces = 15
		}

		public enum SettingValue
		{
			Unkown = -1,
			No = 00000000,
			Yes = 01000000,
			Private = 02000000,
			Public = 04000000
		}

		public SettingType Type { get; set; }
		public int ValueSize { get; set; }
		public SettingValue Value { get; set; }
		public string ValueString { get; set; }
	}
}
