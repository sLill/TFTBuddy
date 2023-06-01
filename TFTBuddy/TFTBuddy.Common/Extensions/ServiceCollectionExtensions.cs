using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace TFTBuddy.Common
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Instantiates a service module of <typeparamref name="TServiceModule"/> and registers it to the service container
        /// </summary>
        /// <typeparam name="TServiceModule">The targetted service module</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceModule<TServiceModule>(this IServiceCollection services) where TServiceModule : IServiceModule, new()
        {
            TServiceModule module = new();

            module.AddServices(services);

            return services;
        }

        /// <summary>
        /// Instantiates a service module of <typeparamref name="TServiceModule"/> along with configuration
        /// data of <typeparamref name="TServiceData"/> for granular configuration
        /// </summary>
        /// <typeparam name="TServiceModule">The targetted service module</typeparam>
        /// <typeparam name="TServiceData">Additional data used to help configure the service module</typeparam>
        /// <param name="services"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceModule<TServiceModule, TServiceData>(this IServiceCollection services, [Optional] TServiceData data) where TServiceModule : IServiceModule<TServiceData>, new()
        {
            TServiceModule module = new();

            module.AddServices(services, data);

            return services;
        }
    }
}
