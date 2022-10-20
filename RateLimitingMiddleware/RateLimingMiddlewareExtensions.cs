using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RateLimitingMiddleware.Models;
using System.Diagnostics.CodeAnalysis;

namespace RateLimitingMiddleware
{
    public static class RateLimingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimiting(
            this IApplicationBuilder builder, [NotNull] Action<RatelimitingConfig> options)
        {

            RatelimitingConfig config;
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                config = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<RatelimitingConfig>>().Value;
                options.Invoke(config);
            }
            return builder.UseMiddleware<RateLimiterMiddleware>(config);
        }
    }
}