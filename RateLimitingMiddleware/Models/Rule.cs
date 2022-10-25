namespace RateLimitingMiddleware.Models
{
    public class Rule
    {
        public string EndPoint { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public long Period { get; set; }  
        public long Limit { get; set; }  
    }
}
