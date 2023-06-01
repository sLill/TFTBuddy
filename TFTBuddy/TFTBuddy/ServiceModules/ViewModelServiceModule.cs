using Microsoft.Extensions.DependencyInjection;
using TFTBuddy.Common;
using TFTBuddy.ViewModels;

namespace TFTBuddy
{
    public class ViewModelServiceModule : IServiceModule
    {
        #region Methods..
        public IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddTransient<MainWindowViewModel>();
            return services;
        } 
        #endregion Methods..
    }
}
