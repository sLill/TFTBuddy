namespace TFTBuddy.Core
{
    public interface IRiotWebClient
    {
        #region Methods..
        Task<string> GetAsync(string endpoint);
        #endregion Methods..
    }
}
