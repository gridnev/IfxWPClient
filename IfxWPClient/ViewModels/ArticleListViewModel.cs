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

namespace IfxWPClient.ViewModels
{
    public class ArticleListViewModel: INotifyPropertyChanged
    {
        INewsDataSource _source;
        Timer _updateNewsListTimer;
        public DateTime LastUpdateTime { get; set; }

        private ObservableCollection<Article> _articles;
        public ObservableCollection<Article> Articles
        {
            get { return _articles; }
            set 
            {
                _articles = value;
                NotifyPropertyChanged("Articles");
                //LastUpdateTime = Articles.Max(n => n.CreateDate);

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

        public RelayCommand<string> ClickArticleItemCommand { get; private set; }

        public ArticleListViewModel(INewsDataSource source)
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

            ClickArticleItemCommand = new RelayCommand<string>((articleId) => ClickArticleItem(articleId));
        }

        private void ClickArticleItem(string newsId)
        {
            //var root = App.Current.RootVisual as PhoneApplicationFrame;

            //Uri uri = new Uri(string.Format("/Views/NewsItem.xaml?newsId={0}", newsId), UriKind.Relative);
            //root.Navigate(uri);
        }

        public void LoadArticles(DateTime updatemark)
        {
            //Busy = true;
            this._source.GetFreeArticleList(updatemark, a => Articles = new ObservableCollection<Article>(a));
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
