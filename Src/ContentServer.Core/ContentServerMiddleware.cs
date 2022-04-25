﻿using ContentServer.Core.Internal;

using Microsoft.AspNetCore.Http;

using System.Text.RegularExpressions;

namespace ContentServer.Core
{
    public class ContentServerMiddleware : IMiddleware
    {
        public ContentServerMiddleware(ContentServerOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ContentServerOptions Options { get; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (next == null) throw new ArgumentNullException(nameof(next));

            if (string.Equals(context.Request.Method, HttpMethods.Head, StringComparison.OrdinalIgnoreCase) || string.Equals(context.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                if (this.Options.TryMatchUrl(context.Request.Path.Value, out string? tenant, out string? main, out string? format))
                {

                }                
            }

            await next(context);
        }        
    }
}