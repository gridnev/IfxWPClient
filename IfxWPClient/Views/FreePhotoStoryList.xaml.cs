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
    public partial class FreePhotoStoryList : UserControl
    {
        public FreePhotoStoryList()
        {
            InitializeComponent();
            Loaded += FreePhotoStoryList_Loaded;
        }

        void FreePhotoStoryList_Loaded(object sender, RoutedEventArgs e)
        {
            var model = ((FreePhotoStoryListViewModel)DataContext);
            if (model != null)
            {
                model.LoadPhotoStoryList(model.LastUpdateTime);
            }
        }
    }
}
