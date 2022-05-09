using ContentServer.Core.Internal;
using ContentServer.Core.Tenants;

using Microsoft.AspNetCore.Http;

namespace ContentServer.Core
{
    public class ContentServerMiddleware : IMiddleware
    {
        public ContentServerMiddleware(ContentServerOptions options, ITenantStorage tenantStore)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            TenantStore = tenantStore ?? throw new ArgumentNullException(nameof(tenantStore));
        }

        public ContentServerOptions Options { get; }
        public ITenantStorage TenantStore { get; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (next == null) throw new ArgumentNullException(nameof(next));

            if (string.Equals(context.Request.Method, HttpMethods.Head, StringComparison.OrdinalIgnoreCase) || string.Equals(context.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                if (this.Options.TryMatchUrl(context.Request.Path.Value, out string? tenantId, out string? main, out string? format))
                {
                    Tenant? tenant;
                    if (await this.TenantStore.TryFindAsync(tenantId!, out tenant))
                    {

                    }
                }                
            }

            await next(context);
        }        
    }
}
