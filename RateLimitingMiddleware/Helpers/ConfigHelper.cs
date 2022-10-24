using Microsoft.Extensions.Configuration;
using RateLimitingMiddleware.Models;
using RockLib.Configuration.ObjectFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitingMiddleware.Helpers
{
    public class ConfigHelper
    {

        private static readonly Lazy<ConfigHelper> lazy =
        new(() => new ConfigHelper());

        public static ConfigHelper Instance { get { return lazy.Value; } }
        public IpBaseConfiguration _ipBaseConfiguration;

        private ConfigHelper()
        {
            _ipBaseConfiguration = new IpBaseConfiguration();
        }

        public void ReadConfigurations(IConfiguration configuration)
        {
            var ipConfigSection = configuration.GetSection("RateLimitingRules:IpRules").GetChildren();
            foreach (var ipConfig in ipConfigSection)
            {
                var childArr = ipConfig.GetChildren().ToArray();
                var key = childArr[0].Value;
                var rules = childArr[1].Create<List<Rule>>() ?? new();
                _ipBaseConfiguration.Rules.Add(key, rules);
            }
        }
    }
}
