using TFTBuddy.Core;

namespace TFTBuddy.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields..
        private readonly IRiotWebService _riotWebService;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public MainWindowViewModel(IRiotWebService riotWebService)
        {
            _riotWebService = riotWebService;
        }
        #endregion Constructors..

        #region Methods..
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var serverData = await _riotWebService.GetServerStatusAsync();
            var summonerData = await _riotWebService.GetSummonerBySummonerNameAsync("");
            var challengerLeagueData = await _riotWebService.GetChallengerLeague();
            var playerMatchesData = await _riotWebService.GetMatchIdsByPUUID("", 20);
            var matchData = await _riotWebService.GetMatchByMatchId("");
        }
        #endregion Methods..
    }
}
