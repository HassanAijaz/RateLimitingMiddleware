using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private Dictionary<string, string> buckets = new();
        public RateLimiterMiddleware(RequestDelegate next,
            RatelimitingConfig config)
        {
            _next = next;
            _config = config;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            foreach (var item in ipConfigurations.Rules)
            {
                foreach (var rule in item.Value)
                {
                    //buckets.Add(item.Key)
                }
            }
            await _next(context);
            return;
        }
    }
}