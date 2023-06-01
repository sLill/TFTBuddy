namespace TFTBuddy.Common
{
    public interface IViewModel : IDisposable
    {
        #region Methods..
        Task InitializeAsync();
        #endregion Methods..
    }
}
