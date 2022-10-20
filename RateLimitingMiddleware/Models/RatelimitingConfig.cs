using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitingMiddleware.Models
{
    public class RatelimitingConfig
    {
        public RateLimitingAlgo RateLimitingAlgo { get; set; }
        public BaseOn BaseOn { get; set; }
    }

    public enum RateLimitingAlgo
    {
        TokenBucket,
        LeakyBucket
    }

    public enum BaseOn
    {
        Ip,
        Client
    }
}
