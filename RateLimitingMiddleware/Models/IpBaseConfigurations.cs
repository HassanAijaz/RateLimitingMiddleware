using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitingMiddleware.Models
{
    public class IpBaseConfiguration    
    {
        public Dictionary<string, List<Rule>> Rules { get; set; } = new ();
    }
}
