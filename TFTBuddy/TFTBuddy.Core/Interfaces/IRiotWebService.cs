﻿using TFTBuddy.Common;
using TFTBuddy.Data;

namespace TFTBuddy.Core
{
    public interface IRiotWebService : IService
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

        Task<ServerStatusData> TFT_GetServerStatusAsync();

        Task<SummonerData> TFT_GetSummonerBySummonerNameAsync(string summonerName);

        Task<ChallengerLeagueData> TFT_GetChallengerLeague();

        Task<PlayerMatchesData> TFT_GetMatchIdsByPUUID(string puuid, int count);

        Task<MatchData> TFT_GetMatchByMatchId(string matchId);
        #endregion Methods..
    }
}
