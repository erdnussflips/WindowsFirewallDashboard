using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Utils
{
	public static class CollectionUtils
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		public enum ArithmeticalLogic
		{
			AND, OR
		}

		public static bool Contains<TValue>(this ICollection<TValue> collection, ArithmeticalLogic logic, params TValue[] values)
		{
			Func<bool, bool, bool> logicalFunction;
			bool startValue;

			switch (logic)
			{
				case ArithmeticalLogic.OR:
					startValue = false;
					logicalFunction = (@old, @new) => @old || @new;
					break;
				case ArithmeticalLogic.AND:
				default:
					startValue = true;
					logicalFunction = (@old, @new) => @old && @new;
					break;
			}


			foreach (var item in values)
			{
				startValue = logicalFunction(startValue, collection.Contains(item));
			}

			return startValue;
		}

		public static TValue GetValue<TKey, TValue> (this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			TValue value;

			try
			{
				if (dictionary.TryGetValue(key, out value))
				{
					return value;
				}
			}
			catch (Exception ex)
			{
				LOG.Debug(ex.Message);
			}

			return defaultValue;
		}

		public static TKey GetKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value, TKey defaultKey)
		{
			try
			{
				foreach (var item in dictionary)
				{
					if (item.Value.GetHashCode() == value.GetHashCode())
					{
						return item.Key;
					}
				}
			}
			catch (Exception ex)
			{
				LOG.Warn(ex.Message);
			}

			return defaultKey;
		}

		public static void Add(this IList list, params object[] items)
		{
			foreach (var item in items)
			{
				list.Add(item);
			}
		}

		public static void AddRange(this IList list, IList items)
		{
			foreach (var item in items)
			{
				list.Add(item);
			}
		}

		public static string Stringify(this IList list)
		{
			var castedList = (list as IList).Cast<object>().ToList();
			var stringBuilder = new StringBuilder();

			for (int i = 0; i < castedList.Count; i++)
			{
				var item = castedList.ElementAt(i);
				var isLastItem = i == castedList.Count - 1;

				stringBuilder.Append(item.ToString());

				if (!isLastItem)
				{
					stringBuilder.Append(", ");
				}
			}

			return stringBuilder.ToString();
		}

		public static int CompareTo<TType>(this IList<TType> left, IList<TType> right) where TType : IComparable
		{
			var minLength = Math.Min(left.Count, right.Count);

			for (int i = 0; i < minLength; i++)
			{
				var itemLeft = left.ElementAt(i);
				var itemRight = right.ElementAt(i);

				var result = itemLeft.CompareTo(itemRight);

				if (result == 0)
				{
					continue;
				}

				return result;
			}

			return left.Count.CompareTo(right.Count);
		}
	}
}
