using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace IfxWPClient.ViewModels
{
    public class ArticleViewModel: INotifyPropertyChanged
    {
        public string Headline { get; set; }

        public DateTime CreateDate { get; set; }

        private Uri _image;
        public Uri Image 
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                NotifyPropertyChanged("Image");
            }
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
