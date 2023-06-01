using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TFTBuddy.Common;
using TFTBuddy.Core;

namespace TFTBuddy
{
    public class ConfigurationServiceModule : IServiceModule<IConfiguration>
    {
        public IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TFTBuddyConfiguration>(configuration.GetSection("TFTBuddyConfiguration"));
            return services;
        }
    }
}
