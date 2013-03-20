using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IfxWPClient.ViewModels;

namespace IfxWPClient.Views
{
    public partial class NewsList : UserControl
    {
        public NewsList()
        {
            InitializeComponent();
            Loaded += NewsList_Loaded;
        }

        void NewsList_Loaded(object sender, RoutedEventArgs e)
        {
            var model = ((NewsListViewModel)DataContext);
            if (model != null)
                model.LoadNews(DateTime.Now.AddHours(-24));
        }
    }
}
