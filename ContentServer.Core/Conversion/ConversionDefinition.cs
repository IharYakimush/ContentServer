using ContentServer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public class ConversionDefinition
    {
        [JsonConstructor]
        public ConversionDefinition(string name, IReadOnlyDictionary<string, string> values)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            Values = values;
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("values")]
        public IReadOnlyDictionary<string, string> Values { get; }

        public string GetHash()
        {
            return HashHelper.HashMd5(this.Name, this.Values.OrderBy(p => p.Key).Select(p => p.Key + p.Value));
        }
    }
}
