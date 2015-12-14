using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public abstract class FirewallObject
	{
		public enum Profile
		{
			Unkown = int.MinValue,
			Domain = 1,
			Private = 2,
			Public = 4
		}

		public Profile Profiles { get; set; }
	}
}
