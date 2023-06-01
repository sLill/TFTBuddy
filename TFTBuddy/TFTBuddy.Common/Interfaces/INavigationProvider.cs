﻿using System.Windows.Controls;

namespace TFTBuddy.Common
{
    public interface INavigationProvider
    {
        #region Methods..
        Task NavigateAsync<TViewModel>() where TViewModel : class;

        void GoBack();

        void Register<TViewModel, TView>() where TViewModel : class
                                           where TView : Control;
        #endregion Methods..
    }
}
