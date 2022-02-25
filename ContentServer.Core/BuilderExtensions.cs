using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ContentServer.Core
{
    public static class BuilderExtensions
    {
        public const string ConfigSectionName = "ContentServer";
        public static IServiceCollection AddContentServer(this IServiceCollection services, Action<ContentServerOptions>? setupOptions = null)
        {
            services.TryAddSingleton(sp =>
            {
                ContentServerOptions options = new ContentServerOptions();
                IConfiguration? configuration = sp.GetService<IConfiguration>();
                configuration?.GetSection(ConfigSectionName)?.Bind(options);
                setupOptions?.Invoke(options);

                return options;
            });

            services.TryAddSingleton<ContentServer>();
            
            return services;
        }

        public static IApplicationBuilder UseContentServer(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ContentServer>();
            return builder;
        }
    }
}
