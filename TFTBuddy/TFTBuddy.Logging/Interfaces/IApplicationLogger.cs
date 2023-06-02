namespace TFTBuddy.Logging
{
    public interface IApplicationLogger
    {
        #region Methods..
        void LogException(Exception ex);

        void Log(string message);
        #endregion Methods..
    }
}
