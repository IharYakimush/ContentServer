using ContentServer.Core.Conversion;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConversionServer.Core;
using Xunit;
using Xunit.Abstractions;

namespace ContentServer.Tests.Conversion
{
    public class ConversionDefinitionTests
    {
        public ConversionDefinitionTests(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public ITestOutputHelper Output { get; }

        [Fact]
        public void Serialize()
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            ConversionDefinition value = new ConversionDefinition("qwe", new Dictionary<string, string>() { { "p1", "v1" }, { "p2", "v2" } });

            string json = JsonSerializer.SerializeToDocument(value).RootElement.ToString();
            Assert.NotNull(json);

            this.Output.WriteLine(json);

            var clone = JsonSerializer.Deserialize<ConversionDefinition>(json, options);
            Assert.NotNull(clone);            
        }
    }
}
