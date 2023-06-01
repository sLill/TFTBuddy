using TFTBuddy.Core;

namespace TFTBuddy.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields..
        private readonly IRiotApiClient _riotApiClient;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public MainWindowViewModel(IRiotApiClient riotApiClient)
        {
            _riotApiClient = riotApiClient;
        }
        #endregion Constructors..

        #region Methods..
        public override Task InitializeAsync()
        {
            return base.InitializeAsync();
        }
        #endregion Methods..
    }
}
