using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RateLimiterMiddleware;
using RateLimitingMiddleware.Enums;
using RateLimitingMiddleware.Helpers;
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

                IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                ConfigHelper.Instance.ReadConfigurations(configuration);
            }
            var buckets = CreateBuckets();
            return builder.UseMiddleware<RateLimiterMiddleware>(config, buckets);
        }

        private static Dictionary<Tuple<string, string, string>, IRateLimitingAlgo> CreateBuckets()
        {
            Dictionary<Tuple<string, string, string>, IRateLimitingAlgo> bucketDic = new();
            foreach (var rule in ConfigHelper.Instance._ipBaseConfiguration.Rules)
            {
                foreach (var ruleConfig in rule.Value)
                {
                    var key = BucketKeyHelper.CreateKey(rule.Key, ruleConfig.EndPoint, ruleConfig.Method);
                    bucketDic.Add(key, new TokenBucket(ruleConfig.Limit, (long)TimeSpan.FromSeconds(ruleConfig.Period).TotalMilliseconds));
                }
            }
            return bucketDic;
        }
    }
}