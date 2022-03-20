using ContentServer.Core.Conversion;

using Microsoft.VisualStudio.TestPlatform.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace ContentServer.Tests.Internal
{
    public class ConversionStepExtensionsTests
    {
        private static Dictionary<string, FileDefinition> inputSingle = new Dictionary<string, FileDefinition>() { { "0", new FileDefinition("any", "any", "jpg") } };
        private static Dictionary<string, ConversionAction> actions = new List<ConversionAction>() { new DefaultConversionAction() }.ToDictionary(a => a.Name);

        public ConversionStepExtensionsTests(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public ITestOutputHelper Output { get; }

        [Theory]
        [InlineData("0", "jpg", "default", "png", true, "png_f9d107e0633af8c9ccb470d775939412")]//change format
        [InlineData("0", "jpg", "default", null, true, "jpg_f9d107e0633af8c9ccb470d775939412")]//change format
        public void Validate_SingleStep_SingleInput(string inpAlias, string inpFormat, string a, string f, bool result, string details)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (f != null)
            {
                values.Add("f", f);
            }

            ConversionStep step1 = new ConversionStep(new ConversionDefinition(a, values), "0");
            ConversionPipeline pipeline = new ConversionPipeline(new[] { step1 }, new Dictionary<string, FileDefinition>() { { inpAlias, new FileDefinition("any", "any", inpFormat) } });

            bool validaion = pipeline.Validate(actions, out var output, out string? description);
            this.Output.WriteLine(output?.ToString() ?? string.Empty);
            this.Output.WriteLine(description ?? string.Empty);

            Assert.Equal(result, validaion);

            if (result)
            {
                Assert.Equal(details, $"{output!.Format}_{output!.Etag}");
            }
            else
            {
                Assert.Equal(details, description!);
            }
        }        

        [Fact]
        public void Serialize()
        {
            ConversionStep toPng = new ConversionStep(new ConversionDefinition("default", new Dictionary<string, string> { { "f", "jpg" } }), "0");
            ConversionStep toJpg = new ConversionStep(new ConversionDefinition("default", new Dictionary<string, string> { { "f", "png" } }), toPng.Output);
            List<ConversionStep> steps = new List<ConversionStep>() { toPng, toJpg };

            JsonDocument? result = JsonSerializer.SerializeToDocument(steps);
            Assert.NotNull(result);

            this.Output.WriteLine(result.RootElement.ToString());

            List<ConversionStep> clone = result.Deserialize<List<ConversionStep>>();
            Assert.NotNull(clone);

            Assert.Equal(steps.Count, clone.Count);
        }
    }
}
