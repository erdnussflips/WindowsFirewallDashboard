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
			WindowsFirewallActivating = 1,
			DeactivatedInterfaces = 15
		}

		public SettingType Type { get; set; }
		public int ValueSize { get; set; }
		public int Value { get; set; }
		public string ValueString { get; set; }
	}
}
