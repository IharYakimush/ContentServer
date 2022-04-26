using System.Security.Cryptography;
using System.Text;

namespace ConversionServer.Core
{
    public static class HashHelper
    {
#pragma warning disable CA1308 // Normalize strings to uppercase
#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
        public static string HashMd5(string value) => Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(value))).ToLowerInvariant();
#pragma warning restore CA5351 // Do Not Use Broken Cryptographic Algorithms
#pragma warning restore CA1308 // Normalize strings to uppercase
        public static string HashMd5(string value, IEnumerable<string> values) => HashHelper.HashMd5(value + string.Join(' ', values));
        public static string HashMd5(string value, params string[] values) => HashHelper.HashMd5(value, (IEnumerable<string>)values);
    }
}
