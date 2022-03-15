using System.Text.RegularExpressions;

namespace ContentServer.Core.Conversion
{
    public class ParamDefinition
    {
        public ParamDefinition(string name, Regex valueValidation)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            ValueValidation = valueValidation ?? throw new ArgumentNullException(nameof(valueValidation));
        }
        public string Name { get; }
        public Regex ValueValidation { get; }

        public override bool Equals(object? obj)
        {
            return obj is ParamDefinition definition &&
                   Name == definition.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }    
}
