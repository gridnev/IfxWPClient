using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;
using Autofac;
using Microsoft.Phone.Controls;

namespace IfxWPClient.Infrastructure
{
    public class AutofacViewModelFactory : IViewModelFactory
    {
        readonly IContainer _container;

        readonly IDictionary<object, ILifetimeScope> _viewsToContainers = new Dictionary<object, ILifetimeScope>();
        readonly object _viewModelToContainersSyncLock = new object();
        private readonly PhoneApplicationFrame _frame;

        public AutofacViewModelFactory(IContainer container)
        {
            _container = container;
            _frame = _container.Resolve<PhoneApplicationFrame>();
            _frame.Navigating += FrameNavigating;
        }

        public T Create<T>()
        {
            var requestScope = _container.BeginLifetimeScope(builder =>
                    builder.RegisterType(typeof(T)).AsSelf()
                );
            var viewModel = requestScope.Resolve<T>();

            lock (_viewModelToContainersSyncLock)
            {
                _viewsToContainers[viewModel] = requestScope;
            }

            return viewModel;
        }

        public void Release(object viewModel)
        {
            var viewsToContainer = _viewsToContainers[viewModel];
            lock (_viewModelToContainersSyncLock)
            {
                _viewsToContainers.Remove(viewModel);
            }

            // Disposing the container will dispose any of the components
            // created within it's lifetime scope.
            viewsToContainer.Dispose();
        }

        void FrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (e.Uri.ToString() == "app://external/" || e.Cancel)
                    return;

                var currentView = GetContentPage();
                if (currentView != null && currentView.DataContext != null)
                {
                    Debug.WriteLine(string.Format("Releasing scope for {0}", currentView.DataContext.GetType().Name));
                    Release(currentView.DataContext);
                }
            }
        }

        private Control GetContentPage()
        {
            Control content = null;
            if (_frame != null && _frame.Content != null && (_frame.Content is Control))
                content = (Control)_frame.Content;
            return content;
        }
    }
}