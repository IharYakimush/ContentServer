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
        [MemberData(nameof(GetParseData), "ContentServer.Tests.Conversion.ParseConversionStep.")]
        public void Parse(string resourceName, string content)
        {
            this.Output.WriteLine(resourceName);
            var steps = ConversionStepExtensions.ParseSteps(resourceName).ToArray();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            var result = JsonSerializer.Serialize(steps, options);
            this.Output.WriteLine(result);
            Assert.Equal(
                Regex.Replace(content, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"), 
                Regex.Replace(result, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"));
        }

        public static IEnumerable<object[]> GetParseData(string resourcePrefix)
        {
            foreach (var name in typeof(ConversionStepTests).Assembly.GetManifestResourceNames().Where(s => s.StartsWith(resourcePrefix)))
            {
                yield return new object[] { Path.GetFileNameWithoutExtension(name).Split(".").Last(), new StreamReader(typeof(ConversionStepTests).Assembly.GetManifestResourceStream(name) ?? throw new InvalidOperationException()).ReadToEnd() };
            }
        }
    }
}
