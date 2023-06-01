using TFTBuddy.Common;

namespace TFTBuddy.ViewModels
{
    public class ViewModelBase : IViewModel
    {
        #region Fields..
        private bool _isDisposed;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        #endregion Constructors..

        #region Methods..
        public virtual Task InitializeAsync() 
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Release managed resources here.            
                }

                // Release unmanaged resources here. DbConnections, COM objects, FileStreams, etc.
                // Unsubscribe from events that origianate from an object that will live longer than this class (typically static and singleton classes)

                _isDisposed = true;
            }
        }
        #endregion Methods..
    }
}
