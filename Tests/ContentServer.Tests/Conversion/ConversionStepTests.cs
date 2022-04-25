using ContentServer.Core.Conversion;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace ContentServer.Tests.Conversion
{
    public class ConversionStepTests
    {
        public ConversionStepTests(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public ITestOutputHelper Output { get; }

        [Theory]
        [InlineData("f_jpg")]
        [InlineData("(f_jpg)")]
        [InlineData("f_jpg,w_100")]
        [InlineData("qwe(f_jpg)")]
        [InlineData("qwe(f_jpg,w_100)")]
        public void Parse(string resourceName)
        {
            this.Output.WriteLine(resourceName);
            var steps = ConversionStepExtensions.ParseSteps(resourceName).ToArray();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            var result = JsonSerializer.Serialize(steps, options);
            this.Output.WriteLine(result);
            using StreamReader reader = new StreamReader(typeof(ConversionStepTests).Assembly.GetManifestResourceStream("ContentServer.Tests.Conversion.ParseConversionStep." + resourceName + ".json"));
            Assert.Equal(
                Regex.Replace(reader.ReadToEnd(), "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"), 
                Regex.Replace(result, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"));
        }
    }
}
