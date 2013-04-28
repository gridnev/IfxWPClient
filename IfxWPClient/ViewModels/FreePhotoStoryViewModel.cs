using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Common.Interfaces;
using IfxWPClient.Views;
using Microsoft.Phone.Shell;
using Common;

namespace IfxWPClient.ViewModels
{
    public class FreePhotoStoryViewModel: INotifyPropertyChanged
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

        public FreePhotoStoryViewModel()
        {
            
        }

        public FreePhotoStoryViewModel(INewsDataSource source, BLL.ImageSource imageSource)
        {
            _source = source;
            _imageSource = imageSource;
        }

        public void LoadArticle(string articleId)
        {
            /*Busy = true;
            this._source.GetFreeNewsById(articleId, r =>
                {
                    Headline = r.Headline;
                    CreateDate = r.CreateDate;
                    Content = r.Content;
                    ImageUrl= new Uri(_imageSource.GetImageByArticleId(Convert.ToInt32(articleId), 0));
                    Id = r.Id;
                });*/
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
