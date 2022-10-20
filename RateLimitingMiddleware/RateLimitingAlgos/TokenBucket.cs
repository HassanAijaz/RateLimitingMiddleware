using System.Runtime;

namespace RateLimiterMiddleware
{
    public class TokenBucket : IRateLimitingAlgo
    {
        private static readonly object _syncRoot = new();
        protected long _bucketCapacity;
        protected readonly long _ticksRefillInterval;
        protected long _nextRefillTime;
        //number of tokens left in the bucket
        protected long _tokensLeft;

        // refillInterval  =  seconds in which bucket will reset
        // bucketTokenCapacity = bucket size.

        //if 
        // refillInterval = 5000 ms =  60 sec.
        // bucketTokenCapacity  = 5 
        //then
        // 5 request should be process in 60 seconds or in a min.


        public TokenBucket(long bucketTokenCapacity, long refillInterval)
        {
            if (bucketTokenCapacity <= 0) throw new ArgumentOutOfRangeException("bucketTokenCapacity", "bucket token capacity can not be negative");
            if (refillInterval < 0) throw new ArgumentOutOfRangeException("refillInterval", "Refill interval cannot be negative");
            _bucketCapacity = bucketTokenCapacity;
            _ticksRefillInterval = TimeSpan.FromMilliseconds(refillInterval).Ticks;
            _nextRefillTime = refillInterval;
            _tokensLeft = bucketTokenCapacity;
        }

        public bool Consume(string key, int toConsume, out TimeSpan waitTimeToBucketReset)
        {
            lock (_syncRoot)
            {
                UpdateBucket();
                if (_tokensLeft < toConsume)
                {
                    var timeToIntervalEnd = _nextRefillTime - DateTime.UtcNow.Ticks;
                    if (timeToIntervalEnd < 0) return Consume(key, toConsume, out waitTimeToBucketReset);
                    waitTimeToBucketReset = TimeSpan.FromTicks(timeToIntervalEnd);
                    return false;
                }
                _tokensLeft -= toConsume;
                waitTimeToBucketReset = TimeSpan.Zero;
                return true;
            }
        }

        private void UpdateBucket()
        {
            var currentTime = DateTime.UtcNow.Ticks;

            if (currentTime < _nextRefillTime) return;

            _tokensLeft = _bucketCapacity;
            _nextRefillTime = currentTime + _ticksRefillInterval;

        }

        public long GetRemainingToken()
        {
            return _tokensLeft;
        }
    }
}