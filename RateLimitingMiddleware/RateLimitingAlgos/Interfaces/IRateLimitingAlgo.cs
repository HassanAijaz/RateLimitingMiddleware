namespace RateLimiterMiddleware
{
    public interface IRateLimitingAlgo
    {
        public bool Consume(string key, int toConsume, out TimeSpan waitTimeToBucketReset);
        public long GetRemainingToken();
    }
}