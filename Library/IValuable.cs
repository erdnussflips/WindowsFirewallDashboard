using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Library
{
	public interface IValuable : IComparable
	{
		string DisplayedValue { get; }
	}

	public interface IValuable<TValueType> : IValuable where TValueType : IComparable { }

	public interface IValuableSingle<TValueType> : IValuable<TValueType> where TValueType : IComparable
	{
		TValueType Value { get; }
	}

	public interface IValuableRange<TValueType> : IValuable<TValueType> where TValueType : IComparable
	{
		TValueType Lowest { get; }
		TValueType Highest { get; }
	}

	public interface IValuableFactory<TValueType> where TValueType : IComparable
	{
		IValuableSingle<TValueType> CreateValueSingle(TValueType value);
		IValuableRange<TValueType> CreateValueRange(TValueType lowest, TValueType highest);
		bool ValidateValue(string value);
		TValueType ParseValue(string value);
	}
}
