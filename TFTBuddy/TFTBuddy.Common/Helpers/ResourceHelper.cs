using System.IO;
using System.Reflection;

namespace TFTBuddy.Common
{
    public static class ResourceHelper
    {
        #region Methods..
        public static string GetResourceString(string projectNamespace, string resourceName)
        {
            var assembly = Assembly.Load(projectNamespace);
            using Stream stream = assembly.GetManifestResourceStream($"{projectNamespace}.{resourceName}");
            using StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();

            return result;
        }
        #endregion Methods..    
    }
}
