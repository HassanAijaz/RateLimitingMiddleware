using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RateLimiterMiddleware;
using RateLimitingMiddleware.Helpers;
using RateLimitingMiddleware.Models;
using System.Net;
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
            string ip = context.Connection.RemoteIpAddress.ToString();
            string method = context.Request.Method.ToString();
            string endpoint = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;
            var key = BucketKeyHelper.CreateKey(ip, endpoint, method.ToUpper());
            var isBucketExists = _buckets.TryGetValue(key, out IRateLimitingAlgo? bucket);
            if (isBucketExists && bucket != null)
            {
                if (!bucket.Consume("", 1, out TimeSpan timeSpan))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    await context.Response.WriteAsync($"Api calls quota exceeded! maximum {bucket.Capacity} per {TimeSpan.FromMilliseconds(bucket.RefilRate).TotalSeconds} seconds. Retry after {timeSpan.TotalSeconds}");
                    return;
                }
            }
            await _next(context);
        }
    }
}