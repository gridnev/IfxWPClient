using BLL;
using Common;
using Common.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Phone.Shell;

namespace IfxWPClient.ViewModels
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        INewsDataSource _source;

        private News NewsItem 
        {
            set
            {
                Content = value.Content;
                CreateDate = value.CreateDate;
                Headline = value.Headline;
            }
        }

        private bool Busy
        {
            set
            {
                SystemTray.ProgressIndicator.IsVisible = value;
            }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyPropertyChanged("Content");

                Busy = false;
            }
        }

        private string _headline;
        public string Headline
        {
            get { return _headline; }
            set
            {
                _headline = value;
                NotifyPropertyChanged("Headline");
            }
        }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set
            {
                _createDate = value;
                NotifyPropertyChanged("CreateDate");
            }
        }

        public NewsViewModel(INewsDataSource source)
        {
            _source = source;
        }

        public void LoadNewsItem(string newsId)
        {
            Busy = true;
            this._source.GetFreeNewsById(newsId, r => NewsItem = r);
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
