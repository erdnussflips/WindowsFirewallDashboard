using System;
using System.Collections.Generic;
using System.Data.HashFunction;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Utils
{
    public static class HashTool
    {
        private static MD5 _md5Provider;
        private static MD5 MD5Provider
        {
            get
            {
                if (_md5Provider == null)
                {
                    _md5Provider = System.Security.Cryptography.MD5.Create();
                }

                return _md5Provider;
            }
        }

        private static xxHash _xxhashProvider;
        private static xxHash xxHashProvider
        {
            get
            {
                if (_xxhashProvider == null)
                {
                    _xxhashProvider = new xxHash();
                }

                return _xxhashProvider;
            }
        }

        public static string MD5(string value)
        {
            //Check wether data was passed
            if ((value == null))
            {
                return null;
            }

            /*//Calculate MD5 hash. This requires that the string is splitted into a byte[].
            var textToHash = Encoding.Default.GetBytes(value);
            var result = MD5Generator.ComputeHash(textToHash);

            //Convert result back to string.
            return BitConverter.ToString(result);*/

            var hash = new StringBuilder();
            var bytes = MD5Provider.ComputeHash(new UTF8Encoding().GetBytes(value));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string xxHash(string value)
        {
            if ((value == null))
            {
                return null;
            }

            var hash = new StringBuilder();
            var bytesHash = xxHashProvider.ComputeHash(value);

            for (int i = 0; i < bytesHash.Length; i++)
            {
                hash.Append(bytesHash[i].ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
