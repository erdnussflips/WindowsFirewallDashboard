using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Library
{
	public interface IComparableList<TType> : IList<TType>, IComparable where TType : IComparable
	{
	}
}
