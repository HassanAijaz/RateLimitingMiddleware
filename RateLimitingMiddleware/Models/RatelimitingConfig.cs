using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateLimitingMiddleware.Enums;

namespace RateLimitingMiddleware.Models
{
    public class RatelimitingConfig
    {
        public RateLimitingAlgo RateLimitingAlgo { get; set; }
        public BaseOn BaseOn { get; set; }
    }
}
