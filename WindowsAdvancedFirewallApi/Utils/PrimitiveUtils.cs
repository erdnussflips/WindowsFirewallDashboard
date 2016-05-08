using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Utils
{
	public static class PrimitiveUtils
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		public static bool ConvertToBool(this int value)
		{
			return value > 0;
		}

		public static int ConvertToInteger(this bool value)
		{
			return value ? 1 : 0;
		}

		public static bool IsNumber(this string value)
		{
			double number;
			return double.TryParse(value, out number);
		}

		public static int ParseInteger(this string value)
		{
			try
			{
				return int.Parse(value);
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info($"Primitive parse error: {value}");
				LOG.Debug(ex);
				throw;
			}
		}

		public static int? ParseInteger(this string value, int? defaultValue)
		{
			int parsed;

			if (int.TryParse(value, out parsed))
			{
				return parsed;
			}

			LOG.Info($"Return default value: {defaultValue}");
			return defaultValue;
		}

		public static int ParseInteger(this string value, int defaultValue)
		{
			return (int)ParseInteger(value, (int?)defaultValue);
		}

		public static long ParseLong(this string value)
		{
			try
			{
				return long.Parse(value);
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info($"Primitive parse error: {value}");
				LOG.Debug(ex);
				throw;
			}
		}

		public static long ParseLong(this string value, long defaultValue)
		{
			long parsed;

			if (long.TryParse(value, out parsed))
			{
				return parsed;
			}

			LOG.Info($"Return default value: {defaultValue}");
			return defaultValue;
		}

		public static float? ParseFloat(this string value, float? defaultValue)
		{
			float parsed;

			if (float.TryParse(value, out parsed))
			{
				return parsed;
			}

			LOG.Info($"Return default value: {defaultValue}");
			return defaultValue;
		}

		public static double? ParseDouble(this string value, double? defaultValue)
		{
			double parsed;

			if (double.TryParse(value, out parsed))
			{
				return parsed;
			}

			LOG.Info($"Return default value: {defaultValue}");
			return defaultValue;
		}
	}
}
