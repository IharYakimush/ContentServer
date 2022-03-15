using ContentServer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public class ConversionDefinition
    {
        //public static ConversionDefinition Original { get; } = new ConversionDefinition("original", new Dictionary<string, string>());
        public ConversionDefinition(string name, Dictionary<string, string> values)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            Values = values;
        }
        public string Name { get; }

        public IReadOnlyDictionary<string, string> Values { get; }

        public string Hash => HashHelper.HashMd5(this.Name, this.Values.OrderBy(p => p.Key).Select(p => p.Key + p.Value));
    }
}
