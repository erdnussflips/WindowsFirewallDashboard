using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Library
{
	public class ComparableList<TType> : List<TType>, IComparableList<TType> where TType : IComparable
	{
		//
		// Zusammenfassung:
		//     Initialisiert eine neue, leere Instanz der System.Collections.Generic.List`1-Klasse,
		//     die die Standardanfangskapazität aufweist.
		public ComparableList() { }

		//
		// Zusammenfassung:
		//     Initialisiert eine neue, leere Instanz der System.Collections.Generic.List`1-Klasse,
		//     die die angegebene Anfangskapazität aufweist.
		//
		// Parameter:
		//   capacity:
		//     Die Anzahl von Elementen, die anfänglich in der neuen Liste gespeichert werden
		//     können.
		//
		// Ausnahmen:
		//   T:System.ArgumentOutOfRangeException:
		//     capacity ist kleiner als 0.
		public ComparableList(int capacity) : base(capacity) { }

		//
		// Zusammenfassung:
		//     Initialisiert eine neue Instanz der System.Collections.Generic.List`1-Klasse,
		//     die aus der angegebenen Auflistung kopierte Elemente enthält und eine ausreichende
		//     Kapazität für die Anzahl der kopierten Elemente aufweist.
		//
		// Parameter:
		//   collection:
		//     Die Auflistung, deren Elemente in die neue Liste kopiert werden.
		//
		// Ausnahmen:
		//   T:System.ArgumentNullException:
		//     collection ist null.
		public ComparableList(IEnumerable<TType> collection) : base(collection) { }

		public int CompareTo(object obj)
		{
			if (obj is ComparableList<TType>)
			{
				var castedObj = obj as ComparableList<TType>;

				return CollectionUtils.CompareTo(this, castedObj);
			}

			return -1;
		}
	}
}
