using Microsoft.Extensions.DependencyInjection;
using TFTBuddy.Common;

namespace TFTBuddy
{
    public class ProviderServiceModule : IServiceModule
    {
        #region Methods..
        public IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton<INavigationProvider, NavigationProvider>()
                    .AddSingleton<IMessageProvider, MessageProvider>();

            return services;
        } 
        #endregion Methods..
    }
}