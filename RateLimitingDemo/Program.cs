using RateLimitingMiddleware;
using RateLimitingMiddleware.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RateLimiterDemo_";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiting(options =>
{
    options.RateLimitingAlgo = RateLimitingAlgo.TokenBucket;
    options.BaseOn = BaseOn.Ip;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
