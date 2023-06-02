using Newtonsoft.Json;
using TFTBuddy.Common;
using TFTBuddy.Configuration;
using TFTBuddy.Data;
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
        public async Task<DataDragonVersionData> DataDragon_GetDataDragonVersionHistoryAsync()
        {
            var dataDragonVersions = await DataDragon_GetDataAsync<List<string>>($"https://ddragon.leagueoflegends.com/api/versions.json", null);
            var dataDragonVersionData = new DataDragonVersionData() { Versions = dataDragonVersions };
            return dataDragonVersionData;
        }

        public async Task<RealmVersionData> DataDragon_GetRealmVersionHistoryAsync()
            => await DataDragon_GetDataAsync<RealmVersionData>($"https://ddragon.leagueoflegends.com/realms/na.json", null);

        public async Task<LanguagesData> DataDragon_GetLanguagesAsync()
        {
            var languages = await DataDragon_GetDataAsync<List<string>>($"https://ddragon.leagueoflegends.com/cdn/languages.json", null);
            var languagesData = new LanguagesData() { Languages = languages};
            return languagesData;
        }

        public async Task<AugmentData> DataDragon_TFT_GetAugmentDataAsync()
        {
            var cacheUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-augments.json"));
            return await DataDragon_GetDataAsync<AugmentData>($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-augments.json", cacheUri);
        }

        public async Task<ChampionData> DataDragon_TFT_GetChampionDataAsync()
        {
            var cacheUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-champion.json"));
            return await DataDragon_GetDataAsync<ChampionData>($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-champion.json", cacheUri);
        }

        public async Task<HeroAugmentData> DataDragon_TFT_GetHeroAugmentDataAsync()
        {
            var cacheUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-hero-augments.json"));
            return await DataDragon_GetDataAsync<HeroAugmentData>($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-hero-augments.json", cacheUri);
        }

        public async Task<ItemData> DataDragon_TFT_GetItemDataAsync()
        {
            var cacheUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-item.json"));
            return await DataDragon_GetDataAsync<ItemData>($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-item.json", cacheUri);
        }

        public async Task<TacticianData> DataDragon_TFT_GetTacticianDataAsync()
        {
            var cacheUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-tactician.json"));
            return await DataDragon_GetDataAsync<TacticianData>($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-tactician.json", cacheUri);
        }

        public async Task<TraitData> DataDragon_TFT_GetTraitDataAsync()
        {
            var cacheUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchDataDirectory(), "tft-trait.json"));
            return await DataDragon_GetDataAsync<TraitData>($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/data/{_applicationConfiguration.Language}/tft-trait.json", cacheUri);
        }

        public async Task<Uri> DataDragon_TFT_GetArenaImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-arena", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-arena/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetAugmentImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-augment", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-augment/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetAugmentContainerImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-augment-container", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-augment-container/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetChampionImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-champion", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-champion/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetHeroAugmentImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-hero-augment", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-hero-augment/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetItemImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-item", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-item/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetQueueImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-queue", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-queue/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetRegaliaImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-regalia", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-regalia/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetTacticianImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-tactician", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-tactician/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        public async Task<Uri> DataDragon_TFT_GetTraitImageAsync(string imageName)
        {
            var imageUri = new Uri(Path.Combine(_applicationConfiguration.GetPatchImageDirectory(), "tft-trait", $"{imageName}.png"));

            if (!File.Exists(imageUri.AbsolutePath))
                await DataDragon_DownloadAsync($"https://ddragon.leagueoflegends.com/cdn/{_applicationConfiguration.Patch}/img/tft-trait/{imageName}.png", imageUri.AbsolutePath);

            return imageUri;
        }

        private async Task<T> DataDragon_GetDataAsync<T>(string endpoint, Uri? localCacheUri) where T : class
        {
            T result = default;

            try
            {
                // Check and use local data if it has been cached
                if (localCacheUri != null && File.Exists(localCacheUri.AbsolutePath))
                {
                    string dataString = await File.ReadAllTextAsync(localCacheUri.AbsolutePath);
                    result = JsonConvert.DeserializeObject<T>(dataString);
                }
                else
                {
                    var httpClient = new HttpClient();

                    var response = await httpClient.GetAsync(endpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<T>(responseString);

                        // Write data to local cache
                        if (localCacheUri != null)
                            await File.WriteAllTextAsync(localCacheUri.AbsolutePath, responseString);
                    }
                    else
                        _applicationLogger.Log($"GET request to {endpoint} failed with status code {response.StatusCode}");
                }
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
