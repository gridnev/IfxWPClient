using BLL;
using Common;
using Common.Interfaces;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IfxWPClient.Infrastructure;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace IfxWPClient.ViewModels
{
    public class NewsListViewModel:INotifyPropertyChanged
    {
        INewsDataSource _source;
        Timer _updateNewsListTimer;
        public DateTime LastUpdateTime { get; set; }

        private ObservableCollection<News> _news;
        public ObservableCollection<News> News
        {
            get { return _news; }
            set 
            { 
                _news = value;
                NotifyPropertyChanged("News");
                LastUpdateTime = News.Max(n => n.CreateDate);
            }
        }

        public RelayCommand<string> ClickNewsItemCommand { get; private set; }

        public NewsListViewModel(INewsDataSource source)
        {
            _source = source;

            /*_lastUpdateTime = DateTime.Now;
            _updateNewsListTimer = new Timer((e) => 
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    { 
                        LoadNews(_lastUpdateTime);
                    });
            }, null, 0, 30000);*/

            ClickNewsItemCommand = new RelayCommand<string>((newsId) => ClickNewsItem(newsId));
        }

        private void ClickNewsItem(string newsId)
        {
            var root = App.Current.RootVisual as PhoneApplicationFrame;

            Uri uri = new Uri(string.Format("/Views/NewsItem.xaml?newsId={0}", newsId), UriKind.Relative);
            root.Navigate(uri);
        }

        public void LoadNews(DateTime updatemark)
        {
            this._source.GetFreeNewsList(updatemark, r => News = new ObservableCollection<News>(r));
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
