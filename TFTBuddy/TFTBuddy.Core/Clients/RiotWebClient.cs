using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace TFTBuddy.Core
{
    public class RiotWebClient : IRiotWebClient
    {
        #region Fields..
        private readonly IOptionsSnapshot<TFTBuddyConfiguration> _tftBuddyConfig;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public RiotWebClient(IOptionsSnapshot<TFTBuddyConfiguration> tftBuddyConfig)
        {
            _tftBuddyConfig = tftBuddyConfig;
        }
        #endregion Constructors..

        #region Methods..
        public async Task<string> GetAsync(string apiEndpoint)
        {
            string apiKey = _tftBuddyConfig.Value.RiotApiKey;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await httpClient.GetAsync(apiEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
                throw new Exception($"GET request to {apiEndpoint} failed with status code {response.StatusCode}");
        }
        #endregion Methods..
    }
}
