using TFTBuddy.Common;
using TFTBuddy.Core;

namespace TFTBuddy.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields..
        private readonly INavigationProvider _navigationProvider;
        private readonly IRiotWebService _riotWebService;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public MainWindowViewModel(INavigationProvider navigationProvider, IRiotWebService riotWebService)
        {
            _navigationProvider = navigationProvider;
            _riotWebService = riotWebService;
        }
        #endregion Constructors..

        #region Methods..
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await _navigationProvider.NavigateAsync<SettingsViewModel>();
        }

        private async Task GetDataAsync()
        {
            // DataDragon
            var dataDragonVersionData = await _riotWebService.DataDragon_GetDataDragonVersionHistoryAsync();
            var realmVersionData = await _riotWebService.DataDragon_GetRealmVersionHistoryAsync();
            var languagesData = await _riotWebService.DataDragon_GetLanguagesAsync();
            var augmentData = await _riotWebService.DataDragon_TFT_GetAugmentDataAsync();
            var championData = await _riotWebService.DataDragon_TFT_GetChampionDataAsync();
            var heroAugmentData = await _riotWebService.DataDragon_TFT_GetHeroAugmentDataAsync();
            var itemData = await _riotWebService.DataDragon_TFT_GetItemDataAsync();
            var tacticianData = await _riotWebService.DataDragon_TFT_GetTacticianDataAsync();
            var traitData = await _riotWebService.DataDragon_TFT_GetTraitDataAsync();
            var itemUri = await _riotWebService.DataDragon_TFT_GetItemImageAsync("TFT_Item_ArchangelsStaff");

            //TFT 
            var serverStatusResponse = await _riotWebService.TFT_GetServerStatusAsync();
            var summonerData = await _riotWebService.TFT_GetSummonerBySummonerNameAsync("RippStudwell");
            var challengerLeagueData = await _riotWebService.TFT_GetChallengerLeague();
            var playerMatchesData = await _riotWebService.TFT_GetMatchIdsByPUUID("4ZrffQuNm68IePh7fw6dGUoPN7oUrsL8jiwI8X_yehqm2x96V2oxOKJv1nwbBp61WBaN2NhZk-UPFA", 20);
            var matchData = await _riotWebService.TFT_GetMatchByMatchId("NA1_4672622973");
        }
        #endregion Methods..
    }
}
