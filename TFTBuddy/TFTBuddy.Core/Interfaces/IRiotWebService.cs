namespace TFTBuddy.Core
{
    public interface IRiotWebService
    {
        #region Methods..
        Task<string> GetServerStatusAsync();
        #endregion Methods..
    }
}
