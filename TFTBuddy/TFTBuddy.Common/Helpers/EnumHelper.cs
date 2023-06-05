namespace TFTBuddy.Common
{
    public static class EnumHelper
    {
        #region Methods..
        /// <summary>
        /// Return all enum values of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>()
            => Enum.GetValues(typeof(T)).Cast<T>();
        #endregion Methods..
    }
}
