using ContentServer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

            foreach (string item in input)
            {
                if (!inputPattern.IsMatch(item))
                {
                    throw new ArgumentException(nameof(input), $"input value {item} not match {inputPattern} pattern");
                }
            }

            this.Output = HashHelper.HashMd5(this.Conversion.GetHash(), this.Input);
        }

        public ConversionStep(ConversionDefinition conversion, params string[] input):this(conversion,(IReadOnlyCollection<string>)input)
        {
            
        }
        public IReadOnlyCollection<string> Input { get; }
        
        public ConversionDefinition Conversion { get; }

        public string Output { get; }        
    }
    public class ConversionPipeline
    {
        public ConversionPipeline(IReadOnlyDictionary<int, ConversionStep> steps)
        {
            this.Steps = steps;
        }

        public IReadOnlyDictionary<int, ConversionStep> Steps { get; }
    }
}
