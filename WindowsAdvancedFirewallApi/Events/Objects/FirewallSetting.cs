using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Events.Objects
{
	public class FirewallSetting : FirewallBaseObject
	{
		public enum SettingType
		{
			Unkown = -1,
			WindowsFirewallActivating = 1,
			WindowsFirewallCurrentProfile = 2,
			WindowsFirewallShieldMode = 3, // Blocks all incoming connections (inclusive the allowed apps)
			DeactivateIncomingNotification = 10,
			DeactivatedInterfaces = 15,
			OutgoingStandardAction = 16,
			IncomingStandardAction = 17
		}

		public class ValueTypes
		{
			public static List<Type> Collection = new List<Type> { typeof(Common), typeof(StandardAction) };

			public enum Common
			{
				Empty = -2,
				Unkown = -1,
				No = 00000000,
				Yes = 01000000,
				Private = 02000000,
				Public = 04000000
			}

			public enum StandardAction
			{
				Unkown = -1,
				Allowing = 00000000,
				Blocking = 01000000,
			}
		}

		public SettingType Type { get; set; }

		private Enum _value;
		public Enum Value
		{
			get { return _value; }
			set
			{
				if (!ValueTypes.Collection.Contains(value.GetType()))
				{
					throw new ArgumentOutOfRangeException(nameof(ValueType), string.Format("Only enumeration types from class {0} are allowed.", nameof(ValueTypes)));
				}

				_value = value;
				ValueType = value.GetType();
			}
		}
		public Type ValueType { get; private set; }

		public int ValueSize { get; set; }
		public string ValueString { get; set; }
	}
}
