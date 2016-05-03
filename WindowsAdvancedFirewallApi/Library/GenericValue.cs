using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Library
{
	public class GenericValue<TValueType> : IValuableSingle<TValueType> where TValueType : IComparable
	{
		public TValueType Value { private set; get; }

		public string DisplayedValue
		{
			get
			{
				return $"{Value}";
			}
		}

		public GenericValue(TValueType value)
		{
			Ensure.ArgumentNotNull(value, nameof(value));

			Value = value;
		}

		public int CompareTo(object obj)
		{
			if (obj is GenericValue<TValueType>)
			{
				var castedObj = obj as GenericValue<TValueType>;
				return Value?.CompareTo(castedObj.Value) ?? -1;
			}

			return -1;
		}

		public override string ToString()
		{
			return DisplayedValue;
		}
	}

	public class IntegerValue : GenericValue<int>
	{
		public IntegerValue(int value) : base(value) { }
	}

	public class IPAddressValue : GenericValue<IPAddress>
	{
		public IPAddressValue(IPAddress value) : base(value) { }
	}
}
