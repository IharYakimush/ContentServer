using ContentServer.Core.Helpers;

using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ContentServer.Core.Conversion
{
    public class ConversionStep
    {
        private static readonly Regex inputPattern = new Regex("^[a-z,0-9]{1,32}$");

        [JsonConstructor]
        public ConversionStep(ConversionDefinition conversion, IReadOnlyCollection<string> input)
        {
            Conversion = conversion;
            this.Input = new HashSet<string>(input);            

            this.Output = HashHelper.HashMd5(this.Conversion.GetHash(), this.Input);
        }

        public ConversionStep(ConversionDefinition conversion, params string[] input):this(conversion,(IReadOnlyCollection<string>)input)
        {
            
        }
        public IReadOnlyCollection<string> Input { get; }
        
        public ConversionDefinition Conversion { get; }

        public string Output { get; } 
        
        public bool Validate(out string? details)
        {
            foreach (string item in this.Input)
            {
                if (!inputPattern.IsMatch(item))
                {
                    details = $"Input value {item} not match {inputPattern} pattern";
                    return false;
                }
            }

            details = null;
            return true;
        }
    }
}
