using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IfxWPClient.ViewModels;
using Microsoft.Phone.Controls;

namespace IfxWPClient.Views
{
    public partial class ArticlePage : PhoneApplicationPage
    {
        public ArticlePage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ArticlePage_Loaded);
        }

        void ArticlePage_Loaded(object sender, RoutedEventArgs e)
        {
            var model = ((ArticleViewModel)DataContext);
            if (model == null) return;

            string articleId = null;
            if (this.NavigationContext.QueryString.ContainsKey("articleId"))
            {
                articleId = NavigationContext.QueryString["articleId"];
                model.LoadArticle(articleId);
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void WebBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var model = ((ArticleViewModel)DataContext);
            if (model == null) return;
            model.Busy = false;
        }
    }
}