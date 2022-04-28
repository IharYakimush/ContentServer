using PublicApiGenerator;
using Xunit;

namespace ConversionServer.Core.Tests;

public class PublicApiTests
{
    [Fact]
    public void Test()
    {
        string publicApi = typeof(ConversionAction).Assembly.GeneratePublicApi(new ApiGeneratorOptions
        {
            IncludeAssemblyAttributes = false,
        });

        // TODO: verify
    }
}