namespace RateLimitingMiddleware.Models
{
    public class Rule
    {
        public string EndPoint { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public string Limit { get; set; } = string.Empty;
    }
}
