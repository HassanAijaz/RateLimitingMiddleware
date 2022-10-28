using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitingMiddleware.Helpers
{
    public class BucketKeyHelper 
    {
        public static Tuple<string,string,string> CreateKey(string ip, string endPoint, string method)
        {
            return new Tuple<string, string, string>(ip, endPoint, method);
        }
    }
}
