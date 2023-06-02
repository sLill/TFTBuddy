using System.Security.Policy;
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
        #endregion Properties..

        #region Methods..
        string GetPatchDirectory();

        string GetPatchDataDirectory();

        string GetPatchImageDirectory();
        #endregion Methods..
    }
}
