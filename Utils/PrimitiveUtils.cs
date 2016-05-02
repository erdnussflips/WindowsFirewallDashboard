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
		private static Logger LOG = LogManager.GetCurrentClassLogger();

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
				LOG.Info(string.Format("Primitive parse error: {0}", value));
				LOG.Debug(ex);
				throw;
			}
		}

		public static int ParseInteger(this string value, int defaultValue)
		{
			try
			{
				return int.Parse(value);
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info(string.Format("Return default value: {0}", defaultValue));
				return defaultValue;
			}
		}
		public static long ParseLong(this string value)
		{
			try
			{
				return long.Parse(value);
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info(string.Format("Primitive parse error: {0}", value));
				LOG.Debug(ex);
				throw;
			}
		}

		public static long ParseLong(this string value, long defaultValue)
		{
			try
			{
				return long.Parse(value);
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
			{
				LOG.Info(string.Format("Return default value: {0}", defaultValue));
				return defaultValue;
			}
		}
	}
}
