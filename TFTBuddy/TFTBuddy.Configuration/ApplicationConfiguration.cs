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

        #region Language
        private string _language = "en_US";
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }
        #endregion Language

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

        #region Patch
        private string _patch = "13.11.1";
        public string Patch
        {
            get { return _patch; }
            set { _patch = value; }
        }
        #endregion Patch   
        #endregion Properties..

        #region Constructors..
        public ApplicationConfiguration() { }
        #endregion Constructors..

        #region Methods..
        #region Event Handlers..
        #endregion Event Handlers..				

        public string GetPatchDirectory()
        {
            string directory = Path.Combine(_applicationDirectory, Patch);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return directory;
        }

        public string GetPatchDataDirectory()
        {
            string directory = Path.Combine(GetPatchDirectory(), "Data");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return directory;
        }

        public string GetPatchImageDirectory()
        {
            string directory = Path.Combine(GetPatchDirectory(), "Images");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return directory;
        }

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