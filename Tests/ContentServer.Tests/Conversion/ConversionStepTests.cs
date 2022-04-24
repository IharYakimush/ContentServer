using ContentServer.Core.Conversion;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

namespace ContentServer.Tests.Conversion
{
    public class ConversionStepTests
    {
        [Theory]// Chnage result id to be equal etag ??
        [InlineData("")]//jpg to png
        public void Parse(string resourceName)
        {
            var steps = ConversionStepExtensions.Parse(resourceName);

            var result = JsonSerializer.Serialize(steps);
            using StreamReader reader = new StreamReader(typeof(ConversionStepTests).Assembly.GetManifestResourceStream("ContentServer.Tests.Conversion.ParseConversionStep." + resourceName + ".json"));
            Assert.Equal(reader.ReadToEnd(), result);
        }
    }
}
