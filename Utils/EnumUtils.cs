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

		public static string GetEnumValueName(object value, Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(string.Format("{0} must be an enumerated type", nameof(enumType)));
			}

			try
			{
				return Enum.GetName(enumType, value);
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public static string GetEnumValueName(object value)
		{
			return GetEnumValueName(value, value.GetType());
		}

		public static string GetEnumValueName<TEnum>(object value) where TEnum : struct, IConvertible, IComparable, IFormattable
		{
			return GetEnumValueName(value, typeof(TEnum));
		}

		public static TEnum ParseEnum<TEnum>(this short value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible, IComparable, IFormattable => Parse(value, defaultValue);
		public static TEnum ParseEnum<TEnum>(this int value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible, IComparable, IFormattable => Parse(value, defaultValue);
		public static TEnum ParseEnum<TEnum>(this long value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible, IComparable, IFormattable => Parse(value, defaultValue);

		private static TEnum Parse<TEnum>(object value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible, IComparable, IFormattable
		{
			CheckForEnum<TEnum>();

			if(Enum.IsDefined(typeof(TEnum), value))
			{
				var objectValue = value;
				var enumValue = (TEnum)objectValue;
				return enumValue;
			}

			return defaultValue;
		}

		public static TEnum ParseStringValue<TEnum>(string value, TEnum defaultValue = default(TEnum)) where TEnum : struct, IConvertible, IComparable, IFormattable
		{
			CheckForEnum<TEnum>();

			try
			{
				var enumUnderlyingType = Enum.GetUnderlyingType(typeof(TEnum));
				object enumValue = null;

				if (enumUnderlyingType == typeof(short)) enumValue = (short)PrimitiveUtils.ParseLong(value);
				if (enumUnderlyingType == typeof(int)) enumValue = (int)PrimitiveUtils.ParseLong(value);
				if (enumUnderlyingType == typeof(long)) enumValue = PrimitiveUtils.ParseLong(value);

				return Parse(enumValue, defaultValue);
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}
	}
}
