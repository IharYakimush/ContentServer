using System.Text.Json.Serialization;

namespace ConversionServer.Core
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

        public string Name { get; }

        public IReadOnlyDictionary<string, string> Values { get; }

        public string GetHash()
        {
            return GetHash(this.Name,this.Values);
        }

        public static string GetHash(string name, IReadOnlyDictionary<string, string> values)
        {
            return HashHelper.HashMd5(name,values.OrderBy(p => p.Key).Select(p => p.Key + p.Value));
        }
    }
}
