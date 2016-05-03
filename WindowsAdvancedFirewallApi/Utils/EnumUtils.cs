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
				throw new ArgumentException($"{nameof(TEnum)} must be an enumerated type");
			}
		}

		public static string GetEnumValueName(object value, Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException($"{nameof(enumType)} must be an enumerated type");
			}

			try
			{
				return Enum.GetName(enumType, value);
			}
			catch (Exception)
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

				if (enumUnderlyingType == typeof(short)) enumValue = (short)value.ParseLong();
				if (enumUnderlyingType == typeof(int)) enumValue = (int)value.ParseLong();
				if (enumUnderlyingType == typeof(long)) enumValue = value.ParseLong();

				return Parse(enumValue, defaultValue);
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}
	}
}
