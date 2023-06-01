namespace TFTBuddy.Core
{
    public interface IRiotApiClient
    {
        #region Methods..
        Task<string> GetAsync(string endpoint);
        #endregion Methods..
    }
}
