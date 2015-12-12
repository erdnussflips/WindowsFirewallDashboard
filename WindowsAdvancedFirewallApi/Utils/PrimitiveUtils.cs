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

        public static int ParseInteger(this string value, int defaultValue)
        {
            try
            {
                return int.Parse(value);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
            {
                LOG.Info(string.Format("Primitive parse error: {0}", value));
                LOG.Debug(ex);
            }

            LOG.Info(string.Format("Return default value: {0}", defaultValue));
            return defaultValue;
        }
    }
}
