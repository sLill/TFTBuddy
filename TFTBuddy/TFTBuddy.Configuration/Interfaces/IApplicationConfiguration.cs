using TFTBuddy.Common;

namespace TFTBuddy.Configuration
{
    public interface IApplicationConfiguration
    {
        #region Properties..
        string RiotApiKey { get; set; }

        string Language { get; set; }   

        Region Region { get; set; }

        Server Server { get; set; }

        string Patch { get; set; }

        string PatchDirectory { get; }

        string PatchDataDirectory { get; }

        string PatchImageDirectory { get; }
        #endregion Properties..

        #region Methods..
        #endregion Methods..
    }
}
