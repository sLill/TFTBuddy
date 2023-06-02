using TFTBuddy.Common;
using TFTBuddy.Configuration;
using TFTBuddy.Logging;

namespace TFTBuddy.Core
{
    public class RiotWebService : IRiotWebService
    {
        #region Fields..
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly IApplicationLogger _applicationLogger;

        private string _region
        => _applicationConfiguration.Region.GetCustomAttribute<ValueAttribute>().Value.ToString();

        private string _server
            => _applicationConfiguration.Server.GetCustomAttribute<ValueAttribute>().Value.ToString();
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public RiotWebService(IApplicationConfiguration applicationConfiguration, IApplicationLogger applicationLogger)
        {
            _applicationConfiguration = applicationConfiguration;
            _applicationLogger = applicationLogger;
        }
        #endregion Constructors..

        #region Methods..
        public async Task<string> DataDragon_GetDataDragonVersionHistoryAsync()
            => await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/api/versions.json");

        public async Task<string> DataDragon_GetRealmVersionHistoryAsync()
            => await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/realms/na.json");

        public async Task<string> DataDragon_GetLanguagesAsync()
            => await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/cdn/languages.json");

        public async Task<string> DataDragon_TFT_GetAugmentDataAsync()
        {
            string data = string.Empty;
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-augments.json"));

            if (File.Exists(uri.AbsolutePath))
                data = await File.ReadAllTextAsync(uri.AbsolutePath);
            else
            {
                data = await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-augments.json");
                await File.WriteAllTextAsync(uri.AbsolutePath, data);
            }

            return data;
        }

        public async Task<string> DataDragon_TFT_GetChampionDataAsync()
        {
            string data = string.Empty;
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-champion.json"));

            if (File.Exists(uri.AbsolutePath))
               data = await File.ReadAllTextAsync(uri.AbsolutePath);
            else
            {
                data = await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-champion.json");
                await File.WriteAllTextAsync(uri.AbsolutePath, data);
            }

            return data;
        }

        public async Task<string> DataDragon_TFT_GetHeroAugmentDataAsync()
        {
            string data = string.Empty;
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-hero-augments.json"));

            if (File.Exists(uri.AbsolutePath))
                data = await File.ReadAllTextAsync(uri.AbsolutePath);
            else
            {
                data = await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-hero-augments.json");
                await File.WriteAllTextAsync(uri.AbsolutePath, data);
            }

            return data;
        }

        public async Task<string> DataDragon_TFT_GetItemDataAsync()
        {
            string data = string.Empty;
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-item.json"));

            if (File.Exists(uri.AbsolutePath))
                data = await File.ReadAllTextAsync(uri.AbsolutePath);
            else
            {
                data = await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-item.json");
                await File.WriteAllTextAsync(uri.AbsolutePath, data);
            }

            return data;
        }

        public async Task<string> DataDragon_TFT_GetTacticianDataAsync()
        {
            string data = string.Empty;
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-tactician.json"));

            if (File.Exists(uri.AbsolutePath))
                data = await File.ReadAllTextAsync(uri.AbsolutePath);
            else
            {
                data = await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-tactician.json");
                await File.WriteAllTextAsync(uri.AbsolutePath, data);
            }

            return data;
        }

        public async Task<string> DataDragon_TFT_GetTraitDataAsync()
        {
            string data = string.Empty;
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-trait.json"));

            if (File.Exists(uri.AbsolutePath))
                data = await File.ReadAllTextAsync(uri.AbsolutePath);
            else
            {
                data = await DataDragon_GetAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-trait.json");
                await File.WriteAllTextAsync(uri.AbsolutePath, data);
            }

            return data;
        }

        public async Task<Uri> DataDragon_TFT_GetArenaImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-arena", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-arena/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetAugmentImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-augment", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-augment/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetAugmentContainerImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-augment-container", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-augment-container/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetChampionImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-champion", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-champion/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetHeroAugmentImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-hero-augment", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-hero-augment/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetItemImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-item", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-item/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetQueueImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-queue", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-queue/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetRegaliaImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-regalia", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-regalia/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetTacticianImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-tactician", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-tactician/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        public async Task<Uri> DataDragon_TFT_GetTraitImageAsync(string imageName)
        {
            var uri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-trait", $"{imageName}.png"));

            if (!File.Exists(uri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-trait/{imageName}.png", uri.AbsolutePath);

            return uri;
        }

        private async Task<string> DataDragon_GetAsync(string endpoint)
        {
            string result = null;

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                    result = await response.Content.ReadAsStringAsync();
                else
                    throw new Exception($"GET request to {endpoint} failed with status code {response.StatusCode}");
            }
            catch (Exception ex) 
            {
                _applicationLogger.LogException(ex);
            }

            return result;
        }

        private async Task<string> DataDragon_DownloadAsync(string url, string localPath)
        {
            string result = null;

            try
            {
                var directory = Path.GetDirectoryName(localPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var httpClient = new HttpClient();
                
                using (Stream contentStream = await httpClient.GetStreamAsync(url))
                using (Stream fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    await contentStream.CopyToAsync(fileStream);
            }
            catch (Exception ex) 
            { 
                _applicationLogger.LogException(ex); 
            }

            return result;
        }

        public async Task<string> TFT_GetServerStatusAsync()
            => await TFT_GetAsync($"https://{_server}.api.riotgames.com/tft/status/v1/platform-data");

        public async Task<string> TFT_GetSummonerBySummonerNameAsync(string summonerName)
            => await TFT_GetAsync($"https://{_server}.api.riotgames.com/tft/summoner/v1/summoners/by-name/{summonerName}");

        public async Task<string> TFT_GetChallengerLeague()
            => await TFT_GetAsync($"https://{_server}.api.riotgames.com/tft/league/v1/challenger");

        public async Task<string> TFT_GetMatchIdsByPUUID(string puuid, int count)
            => await TFT_GetAsync($"https://{_region}.api.riotgames.com/tft/match/v1/matches/by-puuid/{puuid}/ids?start=0&count={count}");

        public async Task<string> TFT_GetMatchByMatchId(string matchId)
            => await TFT_GetAsync($"https://{_region}.api.riotgames.com/tft/match/v1/matches/{matchId}");

        private async Task<string> TFT_GetAsync(string endpoint)
        {
            string result = null;

            try
            {
                string apiKey = _applicationConfiguration.RiotApiKey;

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept-Language", @"en-US,en;q=0.9");
                httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

                var response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                    result = await response.Content.ReadAsStringAsync();
                else
                    throw new Exception($"GET request to {endpoint} failed with status code {response.StatusCode}");
            }
            catch (Exception ex) 
            {
                _applicationLogger.LogException(ex);
            }

            return result;
        }
        #endregion Methods..
    }
}
