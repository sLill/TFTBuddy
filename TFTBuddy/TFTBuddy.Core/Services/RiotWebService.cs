using TFTBuddy.Common;
using TFTBuddy.Configuration;

namespace TFTBuddy.Core
{
    public class RiotWebService : IRiotWebService
    {
        #region Fields..
        private readonly IApplicationConfiguration _applicationConfiguration;

        private string _region
        => _applicationConfiguration.Region.GetCustomAttribute<ValueAttribute>().Value.ToString();

        private string _server
            => _applicationConfiguration.Server.GetCustomAttribute<ValueAttribute>().Value.ToString();
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public RiotWebService(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }
        #endregion Constructors..

        #region Methods..
        public async Task<string> GetServerStatusAsync()
        {
            var result = await GetAsync($"https://{_server}.api.riotgames.com/tft/status/v1/platform-data");
            return result;
        }

        public async Task<string> GetSummonerBySummonerNameAsync(string summonerName)
        {
            var result = await GetAsync($"https://{_server}.api.riotgames.com/tft/summoner/v1/summoners/by-name/{summonerName}");
            return result;
        }

        public async Task<string> GetChallengerLeague()
        {
            var result = await GetAsync($"https://{_server}.api.riotgames.com/tft/league/v1/challenger");
            return result;
        }

        public async Task<string> GetMatchIdsByPUUID(string puuid, int count)
        {
            var result = await GetAsync($"https://{_region}.api.riotgames.com/tft/match/v1/matches/by-puuid/{puuid}/ids?start=0&count={count}");
            return result;
        }

        public async Task<string> GetMatchByMatchId(string matchId)
        {
            var result = await GetAsync($"https://{_region}.api.riotgames.com/tft/match/v1/matches/{matchId}");
            return result;
        }

        private async Task<string> GetAsync(string apiEndpoint)
        {
            string result = null;

            try
            {
                string apiKey = _applicationConfiguration.RiotApiKey;

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept-Language", @"en-US,en;q=0.9");
                httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

                var response = await httpClient.GetAsync(apiEndpoint);
                if (response.IsSuccessStatusCode)
                    result = await response.Content.ReadAsStringAsync();
                else
                    throw new Exception($"GET request to {apiEndpoint} failed with status code {response.StatusCode}");
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
        #endregion Methods..
    }
}
