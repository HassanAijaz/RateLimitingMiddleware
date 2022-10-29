namespace RateLimiterMiddleware
{
    public interface IRateLimitingAlgo
    {
        public long Capacity { get; }
        public long RefilRate { get; }
        public bool Consume(string key, int toConsume, out TimeSpan waitTimeToBucketReset);
        public long GetRemainingToken();
    }
}