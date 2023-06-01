using TFTBuddy.Core;

namespace TFTBuddy.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields..
        private readonly IRiotWebClient _riotApiClient;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public MainWindowViewModel(IRiotWebClient riotApiClient)
        {
            _riotApiClient = riotApiClient;
        }
        #endregion Constructors..

        #region Methods..
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await _riotApiClient.GetAsync("");
        }
        #endregion Methods..
    }
}
