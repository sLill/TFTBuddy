using System.Net.Http.Headers;
using TFTBuddy.Configuration;

namespace TFTBuddy.Core
{
    public class RiotWebClient : IRiotWebClient
    {
        #region Fields..
        private readonly IApplicationConfiguration _applicationConfiguration;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public RiotWebClient(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }
        #endregion Constructors..

        #region Methods..
        public async Task<string> GetAsync(string apiEndpoint)
        {
            string result = null;

            try
            {
                string apiKey = _applicationConfiguration.RiotApiKey;

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var response = await httpClient.GetAsync(apiEndpoint);
                if (response.IsSuccessStatusCode)
                    result = await response.Content.ReadAsStringAsync();
                else
                    throw new Exception($"GET request to {apiEndpoint} failed with status code {response.StatusCode}");
            }
            catch (Exception ex) { }

            return result;
        }
        #endregion Methods..
    }
}
