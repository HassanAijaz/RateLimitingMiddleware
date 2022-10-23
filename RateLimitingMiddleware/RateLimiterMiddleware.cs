using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RateLimitingMiddleware.Models;
using System.Net.Http;

namespace RateLimitingMiddleware
{
    public class RateLimiterMiddleware 
    {
        private readonly RatelimitingConfig  _config;
        private readonly RequestDelegate _next;
        private IConfiguration _configuration;
        public RateLimiterMiddleware(RequestDelegate next,
            RatelimitingConfig config,
            IConfiguration configuration)
        {
            _next = next;
            _config = config;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            return;
        }
    }
}