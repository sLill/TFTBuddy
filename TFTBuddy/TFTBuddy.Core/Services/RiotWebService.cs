using TFTBuddy.Configuration;

namespace TFTBuddy.Core
{
    public class RiotWebService : IRiotWebService
    {
        #region Fields..
        private readonly IApplicationConfiguration _applicationConfiguration;
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
            var result = await GetAsync(@"https://na1.api.riotgames.com/lol/status/v4/platform-data");
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
