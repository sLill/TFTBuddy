using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Navigation;
using TFTBuddy.Common;
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

            InitializeNavigation(host);

            await host.StartAsync();
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

        private void InitializeNavigation(IHost host)
        {
            var navigationProvider = host.Services.GetService<INavigationProvider>();
            navigationProvider.Register<MainWindowViewModel, MainWindow>();
        }

        private static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<INavigationProvider, NavigationProvider>()
                    .AddSingleton<IMessageProvider, MessageProvider>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddTransient<MainWindowViewModel>();
        }
        #endregion Methods..
    }
}
