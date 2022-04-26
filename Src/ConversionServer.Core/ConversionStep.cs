using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ConversionServer.Core
{
    public class ConversionStep
    {
        private static readonly Regex inputPattern = new Regex("^[a-z,0-9]{1,32}$");

        [JsonConstructor]
        public ConversionStep(ConversionDefinition conversion, string output, IReadOnlyCollection<string> input)
        {
            this.Conversion = conversion;
            this.Input = new HashSet<string>(input);            

            this.Output = output;
            this.ValidatedHash = this.Output;
        }

        public ConversionStep(ConversionDefinition conversion, string output, params string[] input):this(conversion, output, (IReadOnlyCollection<string>)input)
        {
            
        }
        public IReadOnlyCollection<string> Input { get; }
        
        public ConversionDefinition Conversion { get; }

        public string Output { get; }
        
        [JsonIgnore]
        internal string ValidatedHash { get; set; }
        
        public bool Validate(out string? details)
        {
            if (!inputPattern.IsMatch(this.Output))
            {
                details = $"Output alias value {this.Output} not match {inputPattern} pattern";
                return false;
            }

            foreach (string item in this.Input)
            {
                if (!inputPattern.IsMatch(item))
                {
                    details = $"Input alias value {item} not match {inputPattern} pattern";
                    return false;
                }
            }

            details = null;
            return true;
        }
    }
}
