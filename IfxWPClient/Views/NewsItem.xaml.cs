using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IfxWPClient.ViewModels;

namespace IfxWPClient.Views
{
    public partial class NewsItem : PhoneApplicationPage
    {
        public NewsItem()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(NewsItem_Loaded);
        }

        void NewsItem_Loaded(object sender, RoutedEventArgs e)
        {
            var model = ((NewsViewModel)DataContext);
            if (model == null) return;

            string newsId = null;
            if (this.NavigationContext.QueryString.ContainsKey("newsId"))
            {
                newsId = NavigationContext.QueryString["newsId"];
                model.LoadNewsItem(newsId);
            }
        }
    }
}