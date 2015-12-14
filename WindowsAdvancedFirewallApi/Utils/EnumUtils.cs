using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Utils
{
    public static class EnumUtils
    {

        private static void CheckForEnum<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("{0} must be an enumerated type", nameof(TEnum)));
            }
        }

        public static TEnum ParseEnum<TEnum>(this int value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible => ParseEnum(value, defaultValue);

        public static TEnum Parse<TEnum>(int value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            CheckForEnum<TEnum>();

            if(Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)(object)value;
            }

            return defaultValue;
        }

        public static TEnum ParseStringValue<TEnum>(string value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            CheckForEnum<TEnum>();

            try
            {
                var enumValue = PrimitiveUtils.ParseInteger(value);
                return Parse(enumValue, defaultValue);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
