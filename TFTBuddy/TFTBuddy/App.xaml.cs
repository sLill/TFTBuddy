using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TFTBuddy.Common;
using TFTBuddy.Configuration;
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
            .ConfigureServices((hostContext, services) =>
            {
                // Config
                services.AddSingleton<IApplicationConfiguration>(serviceProvider =>
                {
                    var applicationConfiguration = ApplicationConfiguration.Load();
                    Application.Current.Exit += (sender, e) => applicationConfiguration.Save();
                   
                    return applicationConfiguration;
                });

                // Service Modules
                services.AddServiceModule<ViewModelServiceModule>();
                services.AddServiceModule<ProviderServiceModule>();

                // Misc
                services.AddTransient<IRiotWebClient, RiotWebClient>();
            });
        }

        private INavigationProvider InitializeNavigation(IHost host)
        {
            var navigationProvider = host.Services.GetService<INavigationProvider>();
            navigationProvider.Register<MainWindowViewModel, MainWindow>();

            return navigationProvider;
        }
        #endregion Methods..
    }
}
