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
using Microsoft.Phone.Shell;

namespace IfxWPClient.ViewModels
{
    public class NewsListViewModel:INotifyPropertyChanged
    {
        INewsDataSource _source;
        Timer _updateNewsListTimer;
        public DateTime LastUpdateTime { get; set; }

        public bool FirstLoad { get; set; }

        private ObservableCollection<NewsItem> _news;
        public ObservableCollection<NewsItem> News
        {
            get { return _news; }
            set 
            {
                _news = value;
                NotifyPropertyChanged("News");

                //Busy = false;
            }
        }

        private bool Busy
        {
            set
            {
                SystemTray.ProgressIndicator.IsVisible = value;
            }
        }

        public RelayCommand<string> ClickNewsItemCommand { get; private set; }

        public NewsListViewModel(INewsDataSource source)
        {
            _source = source;
            this.News = new ObservableCollection<NewsItem>();
            LastUpdateTime = DateTime.Now.AddHours(-24);
            FirstLoad = true;
            

            /*_updateNewsListTimer = new Timer((e) => 
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    { 
                        LoadNews(LastUpdateTime);
                    });
            }, null, 30000, 30000);*/

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
            Busy = true;
            this._source.GetFreeNewsList(updatemark, r =>{
                r.ForEach(this.News.Add);
                NotifyPropertyChanged("News");
                this.News = new ObservableCollection<NewsItem>(this.News.OrderByDescending(n => n.Id));
                Busy = false;

                if (this.FirstLoad)
                {
                    FirstLoad = false;
                    this.LoadNews(this.LastUpdateTime);
                }
            });

            LastUpdateTime = DateTime.Now;
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
