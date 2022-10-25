using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RateLimiterMiddleware;
using RateLimitingMiddleware.Helpers;
using RateLimitingMiddleware.Models;
using System.Net.Http;

namespace RateLimitingMiddleware
{
    public class RateLimiterMiddleware
    {
        private readonly RatelimitingConfig _config;
        private readonly RequestDelegate _next;
        IpBaseConfiguration ipConfigurations = ConfigHelper.Instance._ipBaseConfiguration;
        private Dictionary<Tuple<string, string, string>, IRateLimitingAlgo> _buckets = new();

        public RateLimiterMiddleware(RequestDelegate next,
            RatelimitingConfig config,
            Dictionary<Tuple<string, string, string>, IRateLimitingAlgo> bucket)
        {
            _next = next;
            _config = config;
            _buckets = bucket;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            await _next(context);
        }
    }
}