using TFTBuddy.Configuration;

namespace TFTBuddy.Logging
{
    public class ApplicationLogger : IApplicationLogger
    {
        #region Member Variables..
        private readonly IApplicationConfiguration _applicationConfiguration;

        // 5 MB
        private long _maxFileSize = 5 * 1024 * 1024;  
        private object _fileLock = new object();

        private string _logFilePath 
            => Path.Combine(_applicationConfiguration.GetLoggingDirectory(), "Log.txt");
        #endregion Member Variables..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public ApplicationLogger(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }
        #endregion Constructors..

        #region Methods..
        private void CheckFileSize()
        {
            // Check file size, truncate if necessary
            var fileInfo = new FileInfo(_logFilePath);
            if (fileInfo.Exists && fileInfo.Length > _maxFileSize)
            {
                // Keep the last 20% of log data
                string[] lines = File.ReadAllLines(_logFilePath);
                int linesToPreserve = (int)(lines.Length * 0.2);
                string[] lastLines = lines.Skip(lines.Length - linesToPreserve).ToArray();

                File.WriteAllLines(_logFilePath, lastLines);
            }
            else
                File.WriteAllText(_logFilePath, string.Empty);
        }

        public void LogException(Exception ex)
            => Log($"{ex.Message} {Environment.NewLine}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}");

        public void Log(string message)
        {
            lock (_fileLock)
            {
                CheckFileSize();
                File.AppendAllText(_logFilePath, $"{Environment.NewLine}> {DateTime.Now} -- {message}");
            }
        }
        #endregion Methods..
    }
}
