using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Library
{
	public class GenericValueRange<TValueType> : IValuableRange<TValueType> where TValueType : IComparable
	{
		public TValueType Lowest { private set; get; }
		public TValueType Highest { private set; get; }

		public string DisplayedValue
		{
			get
			{
				return string.Format("{0}-{1}", Lowest, Highest);
			}
		}

		public GenericValueRange(TValueType lowest, TValueType highest)
		{
			Ensure.ArgumentNotNull(lowest, nameof(lowest));
			Ensure.ArgumentNotNull(highest, nameof(highest));

			Lowest = lowest;
			Highest = highest;
		}

		public bool IsInRange(TValueType value)
		{
			var leftResult = Lowest.CompareTo(value) >= 0;
			var rightResult = Highest.CompareTo(value) <= 0;
			return leftResult && rightResult;
		}

		public int CompareTo(object obj)
		{
			if (obj is GenericValueRange<TValueType>)
			{
				var castedObj = obj as GenericValueRange<TValueType>;
				return 0;
			}

			return -1;
		}

		public override string ToString()
		{
			return DisplayedValue;
		}
	}

	public class IntegerValueRange : GenericValueRange<int>
	{
		public IntegerValueRange(int lowest, int highest) : base(lowest, highest) { }
	}

	public class IPAddressValueRange : GenericValueRange<string>
	{
		public IPAddressValueRange(string lowest, string highest) : base(lowest, highest) { }
	}
}
