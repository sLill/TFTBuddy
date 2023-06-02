using Newtonsoft.Json;
using TFTBuddy.Core;
using TFTBuddy.Data;

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
            //var dataDragonVersionResponse = await _riotWebService.DataDragon_GetDataDragonVersionHistoryAsync();
            //var dataDragonVersions = JsonConvert.DeserializeObject<List<string>>(dataDragonVersionResponse);
            //var dataDragonVersionData = new DataDragonVersionData() { Versions = dataDragonVersions };

            //var realmVersionResponse = await _riotWebService.DataDragon_GetRealmVersionHistoryAsync();
            //var realmVersionData = JsonConvert.DeserializeObject<RealmVersionData>(realmVersionResponse);

            //var languagesResponse = await _riotWebService.DataDragon_GetLanguagesAsync();
            //var languages = JsonConvert.DeserializeObject<List<string>>(languagesResponse);
            //var languagesData = new LanguagesData() { Languages = languages};

            //var augmentResponse = await _riotWebService.DataDragon_TFT_GetAugmentDataAsync();
            //var augmentData = JsonConvert.DeserializeObject<AugmentData>(augmentResponse);

            //var championResponse = await _riotWebService.DataDragon_TFT_GetChampionDataAsync();
            //var championData = JsonConvert.DeserializeObject<ChampionData>(championResponse);

            //var heroAugmentResponse = await _riotWebService.DataDragon_TFT_GetHeroAugmentDataAsync();
            //var heroAugmentData = JsonConvert.DeserializeObject<HeroAugmentData>(heroAugmentResponse);

            //var itemResponse = await _riotWebService.DataDragon_TFT_GetItemDataAsync();
            //var itemData = JsonConvert.DeserializeObject<ItemData>(itemResponse);

            //var tacticianResponse = await _riotWebService.DataDragon_TFT_GetTacticianDataAsync();
            //var tacticianData = JsonConvert.DeserializeObject<TacticianData>(tacticianResponse);

            //var traitResponse = await _riotWebService.DataDragon_TFT_GetTraitDataAsync();
            //var traitData = JsonConvert.DeserializeObject<TraitData>(traitResponse);

            //var itemUri = await _riotWebService.DataDragon_TFT_GetItemImageAsync("TFT_Item_ArchangelsStaff");

            // TFT 
            //var serverStatusResponse = await _riotWebService.TFT_GetServerStatusAsync();
            //var serverStatusData = JsonConvert.DeserializeObject<ServerStatusData>(serverStatusResponse);

            //var summonerResponse = await _riotWebService.TFT_GetSummonerBySummonerNameAsync("RippStudwell");
            //var summonerData = JsonConvert.DeserializeObject<SummonerData>(summonerResponse);

            //var challengerLeagueResponse = await _riotWebService.TFT_GetChallengerLeague();
            //var challengerLeagueData = JsonConvert.DeserializeObject<ChallengerLeagueData>(challengerLeagueResponse);

            //var playerMatchesResponse = await _riotWebService.TFT_GetMatchIdsByPUUID("Aep-CI1TT1VBDHubtYZ1cWwQURvnILya1cB6zUxIiyCHoMf08yuYcafyBuJ2gD5y5eVGvdK69Bl8Qw", 20);
            //var playerMatches = JsonConvert.DeserializeObject<List<string>>(playerMatchesResponse);
            //var playerMatchesData = new PlayerMatchesData() { MatchIds = playerMatches };

            //var matchResponse = await _riotWebService.TFT_GetMatchByMatchId("NA1_4670875706");
            //var matchData = JsonConvert.DeserializeObject<MatchData>(matchResponse);
        }
        #endregion Methods..
    }
}
