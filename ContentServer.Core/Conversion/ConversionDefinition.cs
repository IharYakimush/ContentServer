using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public class ConversionDefinition
    {
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
    }
}
