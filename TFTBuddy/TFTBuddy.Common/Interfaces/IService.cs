namespace TFTBuddy.Common
{
    public interface IService
    {
        #region Methods..
        /// <summary>
        /// </summary>
        /// <param name="interval">Milliseconds</param>
        /// <returns></returns>
        void StartService(int interval);

        void StopService();
        #endregion Methods..
    }
}
