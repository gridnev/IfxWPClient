using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Common.Interfaces;
using IfxWPClient.Views;
using Microsoft.Phone.Shell;

namespace IfxWPClient.ViewModels
{
    public class ArticleViewModel: INotifyPropertyChanged
    {
        private INewsDataSource _source;
        private BLL.ImageSource _imageSource;

        public bool Busy
        {
            set
            {
                SystemTray.ProgressIndicator.IsVisible = value;
            }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
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

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyPropertyChanged("Content");
            }
        }

        private Uri _image;
        public Uri Image 
        {
            get { return _image; }
            set
            {
                _image = value;
                NotifyPropertyChanged("Image");
            }
        }

        public Uri ImageUrl
        {
            set 
            { 
                Content = string.Format("<img src=\"{0}\" style=\"width:100%;\"><br>{1}", value, Content);
            }
        }

        public ArticleViewModel()
        {
            
        }

        public ArticleViewModel(INewsDataSource source, BLL.ImageSource imageSource)
        {
            _source = source;
            _imageSource = imageSource;
        }

        public void LoadArticle(string articleId)
        {
            Busy = true;
            this._source.GetFreeNewsById(articleId, r =>
                {
                    Headline = r.Headline;
                    CreateDate = r.CreateDate;
                    Content = r.Content;
                    ImageUrl= _imageSource.GetImageByArticleId(Convert.ToInt32(articleId), 0);
                    Id = r.Id;
                });
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
