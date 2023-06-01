using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TFTBuddy.Common;
using TFTBuddy.Core;
using TFTBuddy.UI;
using TFTBuddy.ViewModels;

namespace TFTBuddy
{
    public partial class App : Application
    {
        #region Methods..
        #region Event Handlers..
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            var host = CreateHostBuilder().Build();
            await host.StartAsync();

            var navigationProvider = InitializeNavigation(host);
            await navigationProvider.NavigateAsync<MainWindowViewModel>();
        }
        #endregion Event Handlers..

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.Sources.Clear();
                configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                RegisterProviders(services);
                RegisterViewModels(services);
            });
        }

        private INavigationProvider InitializeNavigation(IHost host)
        {
            var navigationProvider = host.Services.GetService<INavigationProvider>();
            navigationProvider.Register<MainWindowViewModel, MainWindow>();

            return navigationProvider;
        }

        private static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<INavigationProvider, NavigationProvider>()
                    .AddSingleton<IMessageProvider, MessageProvider>();
        }

        private static void RegisterClients(IServiceCollection services)
        {
            services.AddTransient<IRiotApiClient, RiotApiClient>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddTransient<MainWindowViewModel>();
        }
        #endregion Methods..
    }
}
