using Microsoft.Extensions.DependencyInjection;
using TFTBuddy.Common;
using TFTBuddy.Core;

namespace TFTBuddy.ServiceModules
{
    public class ServiceServiceModule : IServiceModule
    {
        #region Methods..
        public IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton<IRiotMemoryService, RiotMemoryService>()
                    .AddSingleton<IRiotWebService, RiotWebService>();

            return services;
        }
        #endregion Methods..
    }
}
