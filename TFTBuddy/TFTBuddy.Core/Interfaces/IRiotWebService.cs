namespace TFTBuddy.Core
{
    public interface IRiotWebService
    {
        #region Methods..
        Task<string> DataDragon_GetDataDragonVersionHistoryAsync();

        Task<string> DataDragon_GetRealmVersionHistoryAsync();

        Task<string> DataDragon_GetLanguagesAsync();

        Task<string> DataDragon_TFT_GetArenaDataAsync();

        Task<string> DataDragon_TFT_GetAugmentDataAsync();
     
        Task<string> DataDragon_TFT_GetChampionDataAsync();

        Task<string> DataDragon_TFT_GetHeroAugmentDataAsync();

        Task<string> DataDragon_TFT_GetItemDataAsync();

        Task<string> DataDragon_TFT_GetQueueDataAsync();

        Task<string> DataDragon_TFT_GetRegaliaDataAsync();

        Task<string> DataDragon_TFT_GetTacticianDataAsync();

        Task<string> DataDragon_TFT_GetTraitDataAsync();

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
