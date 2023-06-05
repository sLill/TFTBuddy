using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using TFTBuddy.Common;
using TFTBuddy.Configuration;
using TFTBuddy.Core;
using TFTBuddy.Logging;
using TFTBuddy.ServiceModules;
using TFTBuddy.UI;
using TFTBuddy.UI.Views.UserControls;
using TFTBuddy.ViewModels;

namespace TFTBuddy
{
    public partial class App : Application
    {
        #region Methods..
        #region Event Handlers..
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await InitializeAsync();
        }
        #endregion Event Handlers..

        private async Task InitializeAsync()
        {
            RegisterSyncfusionLicense();

            var host = CreateHostBuilder().Build();
            await host.StartAsync();

            var navigationProvider = InitializeNavigation(host);
            await navigationProvider.NavigateAsync<MainWindowViewModel>();
        }

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
                })

                // Logging
                .AddSingleton<IApplicationLogger, ApplicationLogger>();

                // Service Modules
                services.AddServiceModule<ViewModelServiceModule>();
                services.AddServiceModule<ProviderServiceModule>();
                services.AddServiceModule<ServiceServiceModule>();
            });
        }

        private INavigationProvider InitializeNavigation(IHost host)
        {
            var navigationProvider = host.Services.GetService<INavigationProvider>();

            navigationProvider.Register<MainWindowViewModel, MainWindow>()
                              .Register<SettingsViewModel, SettingsView>();

            return navigationProvider;
        }     

        private void RegisterSyncfusionLicense()
        {
            var syncfusionLicense = ResourceHelper.GetResourceString("TFTBuddy.Resources", @"Licenses.Syncfusion_license.txt");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicense);
        }
        #endregion Methods..
    }
}
