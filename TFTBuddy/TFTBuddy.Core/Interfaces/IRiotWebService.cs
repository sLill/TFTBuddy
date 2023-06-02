namespace TFTBuddy.Core
{
    public interface IRiotWebService
    {
        #region Methods..
        Task<string> GetServerStatusAsync();

        Task<string> GetSummonerBySummonerNameAsync(string summonerName);

        Task<string> GetChallengerLeague();

        Task<string> GetMatchIdsByPUUID(string puuid, int count);

        Task<string> GetMatchByMatchId(string matchId);
        #endregion Methods..
    }
}
