using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Common;
using Common.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace IfxWPClient.ViewModels
{
    public class FreePhotoStoryListViewModel: INotifyPropertyChanged
    {
        INewsDataSource _source;

        BLL.ImageSource _imageSourse;
        Timer _updateNewsListTimer;
        public DateTime LastUpdateTime { get; set; }
        public bool FirstLoad { get; set; }

        private ObservableCollection<FreePhotoStoryViewModel> _freePhotoStoryList;
        public ObservableCollection<FreePhotoStoryViewModel> FreePhotoStoryList
        {
            get { return _freePhotoStoryList; }
            set 
            {
                _freePhotoStoryList = value;
                NotifyPropertyChanged("FreePhotoStoryList");
            }
        }

        private bool Busy
        {
            set
            {
                SystemTray.ProgressIndicator.IsVisible = value;
            }
        }

        public RelayCommand<string> ClickArticleItemCommand { get; private set; }

        public FreePhotoStoryListViewModel(INewsDataSource source, BLL.ImageSource imageSource)
        {
            _source = source;
            _imageSourse = imageSource;
            LastUpdateTime = DateTime.Now.AddDays(-7);
            FreePhotoStoryList = new ObservableCollection<FreePhotoStoryViewModel>();
            FirstLoad = true;

           /* _updateNewsListTimer = new Timer((e) => 
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        LoadArticles(LastUpdateTime);
                    });
            }, null, 300000, 300000);*/

            //ClickArticleItemCommand = new RelayCommand<string>((articleId) => ClickArticleItem(articleId));
        }

        /*private void ClickArticleItem(string articleId)
        {
            var root = App.Current.RootVisual as PhoneApplicationFrame;

            Uri uri = new Uri(string.Format("/Views/ArticlePage.xaml?articleId={0}", articleId), UriKind.Relative);
            root.Navigate(uri);
        }*/

        public void LoadPhotoStoryList(DateTime updatemark)
        {
            this._source.GetFreePhotoStoryList(updatemark,
                                            ps =>
                                            {
                                                ps.Select(ar => new FreePhotoStoryViewModel
                                                      {
                                                          CreateDate = ar.CreateDate,
                                                          Headline = ar.Headline,
                                                          Image = new Uri(ar.ImageUrl),
                                                          Id = ar.Id
                                                      }).ToList().ForEach(FreePhotoStoryList.Add);
                                                NotifyPropertyChanged("FreePhotoStoryList");
                                            });
            LastUpdateTime = DateTime.Now;
            FirstLoad = false;
        }

        #region INPC Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
