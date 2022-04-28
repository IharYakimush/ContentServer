using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace ConversionServer.Core.Tests
{
    public class ConversionStepTests
    {
        public ConversionStepTests(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public ITestOutputHelper Output { get; }

        [Theory]
        [MemberData(nameof(GetParseData), "ConversionServer.Core.Tests.ParseConversionStep.")]
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

        [Theory]
        [InlineData("qwe", "Unable to parse param qwe in qwe for default step")]
        [InlineData("asd(qwe)", "Unable to parse param qwe in qwe for asd step")]
        [InlineData("(qwe)", "Unable to parse param qwe in qwe for default step")]
        [InlineData("t_asd,qwe", "Unable to parse param qwe in t_asd,qwe for default step")]
        public void ParseFailed(string value, string msg)
        {
            this.Output.WriteLine(value);
            var exc = Assert.Throws<ArgumentException>(() => ConversionStepExtensions.ParseSteps(value).ToArray());

            this.Output.WriteLine(exc.Message);
            Assert.Contains(msg, exc.Message);
        }

        [Theory]
        [InlineData("t_%28%29%2C%24", "\"t\": \"(),$\"")]
        public void ParseEscaped(string value, string msg)
        {
            this.Output.WriteLine(value);
            var steps = ConversionStepExtensions.ParseSteps(value).ToArray();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            var result = JsonSerializer.Serialize(steps, options);
            this.Output.WriteLine(result);

            Assert.Contains(msg, result);
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
