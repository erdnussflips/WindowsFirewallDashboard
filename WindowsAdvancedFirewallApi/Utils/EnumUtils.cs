using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Utils
{
	public static class EnumUtils
	{
		private static void CheckForEnum(Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException($"{enumType.Name} must be an enum type");
			}
		}
		private static void CheckForEnum<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
		{
			CheckForEnum(typeof(TEnum));
		}

		public static TAttribute GetAttribute<TEnum, TAttribute>(this TEnum enumValue, int index = 0)
			where TEnum : struct, IConvertible, IComparable, IFormattable
			where TAttribute : Attribute
		{
			CheckForEnum<TEnum>();

			var fi = enumValue.GetType().GetField(enumValue.ToString());
			var attributes = (TAttribute[])fi.GetCustomAttributes(typeof(TAttribute), false);

			return index < attributes?.Count() ? attributes[index] : null;
		}

		public static string GetEnumValueName(object value, Type enumType)
		{
			CheckForEnum(enumType);

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
