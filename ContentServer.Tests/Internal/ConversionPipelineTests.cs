using ContentServer.Core.Conversion;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace ContentServer.Tests.Internal
{
    public class ConversionPipelineTests
    {
        [Fact]
        public void ChangeFormat()
        {
            ConversionPipeline pipeline = new ConversionPipeline(
                new Dictionary<int, ConversionStep>()
                {
                    { 0, new ConversionStep(
                            new ConversionDefinition("default",
                                new Dictionary<string, string>
                                {
                                    {"f","webp" }
                                }), "$0")
                    }
                });
        }
    }
}
