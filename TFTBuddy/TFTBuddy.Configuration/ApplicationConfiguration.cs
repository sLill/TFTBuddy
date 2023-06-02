using Newtonsoft.Json;
using TFTBuddy.Common;

namespace TFTBuddy.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        #region Fields..
        private static string _localAppDataDirectory => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string _applicationDirectory => Path.Combine(_localAppDataDirectory, "TFTBuddy");
        private static string _applicationConfigurationFilePath => Path.Combine(_applicationDirectory, "config.json");
        #endregion Fields..

        #region Properties..
        #region RiotApiKey
        private string _riotApiKey = "REPLACE WITH RIOT API KEY";
        public string RiotApiKey
        {
            get { return _riotApiKey; }
            set { _riotApiKey = value; }
        }
        #endregion RiotApiKey

        #region Region
        private Region _region = Region.AMERICAS;
        public Region Region
        {
            get { return _region; }
            set { _region = value; }
        }
        #endregion Region

        #region Server
        private Server _server = Server.NA1;
        public Server Server
        {
            get { return _server; }
            set { _server = value; }
        }
        #endregion Server
        #endregion Properties..

        #region Constructors..
        public ApplicationConfiguration() { }
        #endregion Constructors..

        #region Methods..
        #region Event Handlers..
        #endregion Event Handlers..				

        public static ApplicationConfiguration Load()
        {
            var applicationConfiguration = new ApplicationConfiguration();

            try
            {
                if (File.Exists(_applicationConfigurationFilePath))
                {
                    var jsonSerializerSettings = new JsonSerializerSettings();
                    applicationConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(_applicationConfigurationFilePath), jsonSerializerSettings);
                }
            }
            catch (Exception ex) { }

            return applicationConfiguration;
        }

        public void Save()
        {
            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings();
                string applicationConfigurationJson = JsonConvert.SerializeObject(this, Formatting.Indented, jsonSerializerSettings);

                if (!Directory.Exists(_applicationDirectory))
                    Directory.CreateDirectory(_applicationDirectory);

                File.WriteAllText(_applicationConfigurationFilePath, applicationConfigurationJson);
            }
            catch (Exception ex) { }
        }
        #endregion Methods..
    }
}