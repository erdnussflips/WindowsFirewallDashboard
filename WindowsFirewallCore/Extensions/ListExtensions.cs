using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.Extensions
{
	public static class ListExtensions
	{
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
		{
			items.ToList().ForEach(collection.Add);
		}
	}
}
