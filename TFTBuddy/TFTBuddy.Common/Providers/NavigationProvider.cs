﻿using System.Windows;
using System.Windows.Controls;

namespace TFTBuddy.Common
{
    public class NavigationProvider : INavigationProvider
    {
        #region Fields..
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Type> _viewModelViewMap = new Dictionary<Type, Type>();
        #endregion Fields..

        #region Constructors..
        public NavigationProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion Constructors..

        #region Methods..
        public async Task NavigateAsync<TViewModel>() where TViewModel : class
        {
            // Locate the corresponding View for the ViewModel
            var viewModelType = typeof(TViewModel);
            IViewModel viewModel = _serviceProvider.GetService(viewModelType) as IViewModel;

            if (_viewModelViewMap.ContainsKey(viewModelType))
            {
                var viewType = _viewModelViewMap[viewModelType];
                var view = (Control)Activator.CreateInstance(viewType);
                view.DataContext = viewModel;

                if (view is Window window)
                    Application.Current.MainWindow = window;
                else
                    Application.Current.MainWindow.Content = view;

                await viewModel.InitializeAsync();
            }
        }

        public void GoBack() { }

        public void Register<TViewModel, TView>() where TViewModel : class
                                                  where TView : Control
        {
            _viewModelViewMap[typeof(TViewModel)] = typeof(TView);
        }
        #endregion Methods..
    }
}
