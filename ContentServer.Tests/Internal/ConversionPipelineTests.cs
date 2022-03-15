using ContentServer.Core.Conversion;

using Microsoft.VisualStudio.TestPlatform.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            ConversionPipeline pipeline = new ConversionPipeline(
                new Dictionary<int, ConversionStep>()
                {
                    { 0, new ConversionStep(
                            new ConversionDefinition("default",
                                new Dictionary<string, string>
                                {
                                    {"f","png" }
                                }), "0")
                    }
                });

            bool result = pipeline.Validate(input, actions, out string? description);

            this.Output.WriteLine(description ?? string.Empty);

            Assert.True(result);
        }
    }
}
