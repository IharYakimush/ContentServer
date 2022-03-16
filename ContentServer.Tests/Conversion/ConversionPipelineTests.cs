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
    public class ConversionPipelineValidationTests
    {
        private static Dictionary<string, string> input = new Dictionary<string, string>() { { "0", "jpg" } };
        private static List<ConversionAction> actions = new List<ConversionAction>() { new DefaultConversionAction() };

        public ConversionPipelineValidationTests(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public ITestOutputHelper Output { get; }

        [Fact]
        public void Validate()
        {
            ConversionStep toPng = new ConversionStep(new ConversionDefinition("default", new Dictionary<string, string> { { "f", "png" } }), "0");
            ConversionStep toJpg = new ConversionStep(new ConversionDefinition("default", new Dictionary<string, string> { { "f", "png" } }), toPng.Output);
            ConversionPipeline pipeline = new ConversionPipeline(
                new Dictionary<int, ConversionStep>()
                {
                    { 0, toPng },
                    { 1, toJpg },
                });

            bool result = pipeline.Validate(input, actions, out string? description);

            this.Output.WriteLine(description ?? string.Empty);

            Assert.True(result);
        }

        [Fact]
        public void Serialize()
        {
            ConversionStep toPng = new ConversionStep(new ConversionDefinition("default", new Dictionary<string, string> { {"f","jpg" } }), "0");
            ConversionStep toJpg = new ConversionStep(new ConversionDefinition("default", new Dictionary<string, string> { { "f", "png" } }), toPng.Output);
            ConversionPipeline pipeline = new ConversionPipeline(
                new Dictionary<int, ConversionStep>()
                {
                    { 0, toPng },
                    { 1, toJpg },
                });

            JsonDocument? result = JsonSerializer.SerializeToDocument(pipeline);
            Assert.NotNull(result);

            this.Output.WriteLine(result.RootElement.ToString());

            ConversionPipeline clone = result.Deserialize<ConversionPipeline>();
            Assert.NotNull(clone);

            Assert.Equal(pipeline.Steps.Count, clone.Steps.Count);

        }
    }
}
