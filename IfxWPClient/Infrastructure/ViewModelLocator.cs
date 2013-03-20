using IfxWPClient.ViewModels;
using System;

namespace IfxWPClient.Infrastructure
{
    public class ViewModelLocator
    {
        private IViewModelFactory _factory;
        public IViewModelFactory Factory
        {
            private get { return _factory; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _factory = value;
            }
        }

        public NewsListViewModel NewsListViewModel
        {
            get { return Factory.Create<NewsListViewModel>(); }
        }

        public NewsViewModel NewsViewModel
        {
            get { return Factory.Create<NewsViewModel>(); }
        }

        public ArticleListViewModel ArticleListViewModel
        {
            get { return Factory.Create<ArticleListViewModel>(); }
        }
    }
}
