using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContentServer.Core.Helpers
{
    public static class HashHelper
    {
        public static string HashMd5(string value) => Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(value)));
        public static string HashMd5(string value, IEnumerable<string> values) => HashHelper.HashMd5(value + string.Join(' ', values));
        public static string HashMd5(string value, params string[] values) => HashHelper.HashMd5(value, values);
    }
}
