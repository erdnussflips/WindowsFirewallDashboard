using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Utils
{
    public static class EnumUtils
    {

        public static TEnum Parse<TEnum>(this int value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("{0} must be an enumerated type", nameof(TEnum)));
            }

            if(Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)(object)value;
            }

            return defaultValue;
        }
    }
}
