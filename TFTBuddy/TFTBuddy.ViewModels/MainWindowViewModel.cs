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

            // DataDragon
            //var versionData = await _riotWebService.DataDragon_GetDataDragonVersionHistoryAsync();
            var itemUri = await _riotWebService.DataDragon_TFT_GetItemImageAsync("TFT_Item_ArchangelsStaff");

            // TFT 
            //var serverData = await _riotWebService.TFT_GetServerStatusAsync();
            //var summonerData = await _riotWebService.TFT_GetSummonerBySummonerNameAsync("RippStudwell");
            //var challengerLeagueData = await _riotWebService.TFT_GetChallengerLeague();
            //var playerMatchesData = await _riotWebService.TFT_GetMatchIdsByPUUID("Aep-CI1TT1VBDHubtYZ1cWwQURvnILya1cB6zUxIiyCHoMf08yuYcafyBuJ2gD5y5eVGvdK69Bl8Qw", 20);
            //var matchData = await _riotWebService.TFT_GetMatchByMatchId("NA1_4670875706");
        }
        #endregion Methods..
    }
}
