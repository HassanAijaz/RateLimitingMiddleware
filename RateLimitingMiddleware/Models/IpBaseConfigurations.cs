using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitingMiddleware.Models
{
    public class IpBaseConfiguration
    {
        public string Ip { get; set; } = string.Empty;
        public ICollection<Rule> Rules { get; set; } = new List<Rule>();
    }
}
