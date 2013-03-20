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

namespace IfxWPClient.Views
{
    public partial class ArticleList : UserControl
    {
        public ArticleList()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ArticleList_Loaded);
        }

        void ArticleList_Loaded(object sender, RoutedEventArgs e)
        {
            var model = ((ArticleListViewModel)DataContext);
            if (model != null)
                model.LoadArticles(DateTime.Now.AddHours(-24));
        }
    }
}
