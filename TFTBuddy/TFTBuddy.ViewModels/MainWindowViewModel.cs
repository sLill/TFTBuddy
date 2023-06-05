using TFTBuddy.Common;
using TFTBuddy.Core;

namespace TFTBuddy.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields..
        private readonly INavigationProvider _navigationProvider;
        private readonly IRiotWebService _riotWebService;
        private readonly IRiotMemoryService _riotMemoryService;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public MainWindowViewModel(INavigationProvider navigationProvider, 
                                   IRiotWebService riotWebService,
                                   IRiotMemoryService riotMemoryService)
        {
            _navigationProvider = navigationProvider;
            _riotWebService = riotWebService;
            _riotMemoryService = riotMemoryService;
        }
        #endregion Constructors..

        #region Methods..
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _riotMemoryService.StartService(2000);
            //_riotWebService.StartService(30000);

            await _navigationProvider.NavigateAsync<SettingsViewModel>();
        }
        #endregion Methods..
    }
}
