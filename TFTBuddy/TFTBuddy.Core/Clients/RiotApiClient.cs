using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace TFTBuddy.Core
{
    public class RiotApiClient : IRiotApiClient
    {
        #region Fields..
        private readonly IConfiguration _configuration;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public RiotApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion Constructors..

        #region Methods..
        public async Task<string> GetAsync(string endpoint)
        {
            string apiKey = _configuration.GetValue<string>("RiotApiKey");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
                throw new Exception($"GET request to {endpoint} failed with status code {response.StatusCode}");
        }
        #endregion Methods..
    }
}
