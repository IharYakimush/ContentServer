using ContentServer.Core;
using ContentServer.Core.Internal;
using Xunit;

namespace ContentServer.Tests.Internal
{
    public class UrlExtensionsTest
    {
        [Theory]
        [InlineData("/tenant/layer1/layer2/conv/alias.format", true, "tenant", "layer1/layer2/conv/alias", "format")]
        public void TryMatchUrl(string url, bool match, string? tenant, string? main, string? format)
        {
            ContentServerOptions options = new ContentServerOptions();

            Assert.Equal(match, options.TryMatchUrl(url, out string? t, out string? m, out string? f));
            Assert.Equal(tenant, t);
            Assert.Equal(main, m);
            Assert.Equal(format, f);
        }
    }
}