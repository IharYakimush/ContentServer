
using System.Text.RegularExpressions;

namespace ContentServer.Core.Internal
{
    internal static class UrlExtensions
    {
        internal static bool TryMatchUrl(this ContentServerOptions options, string value, out string? tenant, out string? main, out string? format)
        {
            Match tenantMatch = options.UrlRegex.Match(value);

            if (tenantMatch.Success)
            {
                tenant = tenantMatch.Result(options.TenantReplacement);
                main = tenantMatch.Result(options.MainReplacement);
                format = tenantMatch.Result(options.FormatReplacement);

                return true;
            }

            tenant = null;
            main = null;
            format = null;

            return false;
        }
    }
}
