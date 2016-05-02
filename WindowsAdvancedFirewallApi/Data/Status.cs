using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Data
{
	public enum Status
	{
		Enabled, Disabled
	}

	internal static class StatusUtil
	{
		public static bool ToBoolean(this Status status)
		{
			return status == Status.Enabled;
		}

		public static Status ToStatusEnum(this bool boolean)
		{
			return boolean ? Status.Enabled : Status.Disabled;
		}
	}
}
