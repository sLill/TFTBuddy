using TFTBuddy.Data;

namespace TFTBuddy.Core
{
    public interface IRiotWebService
    {
        #region Methods..
        Task<DataDragonVersionData> DataDragon_GetDataDragonVersionHistoryAsync();

        Task<RealmVersionData> DataDragon_GetRealmVersionHistoryAsync();

        Task<LanguagesData> DataDragon_GetLanguagesAsync();

        Task<AugmentData> DataDragon_TFT_GetAugmentDataAsync();
     
        Task<ChampionData> DataDragon_TFT_GetChampionDataAsync();

        Task<HeroAugmentData> DataDragon_TFT_GetHeroAugmentDataAsync();

        Task<ItemData> DataDragon_TFT_GetItemDataAsync();

        Task<TacticianData> DataDragon_TFT_GetTacticianDataAsync();

        Task<TraitData> DataDragon_TFT_GetTraitDataAsync();

        Task<Uri> DataDragon_TFT_GetArenaImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetAugmentImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetAugmentContainerImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetChampionImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetHeroAugmentImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetItemImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetQueueImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetRegaliaImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetTacticianImageAsync(string assetName);

        Task<Uri> DataDragon_TFT_GetTraitImageAsync(string assetName);

        Task<string> TFT_GetServerStatusAsync();

        Task<string> TFT_GetSummonerBySummonerNameAsync(string summonerName);

        Task<string> TFT_GetChallengerLeague();

        Task<string> TFT_GetMatchIdsByPUUID(string puuid, int count);

        Task<string> TFT_GetMatchByMatchId(string matchId);
        #endregion Methods..
    }
}
