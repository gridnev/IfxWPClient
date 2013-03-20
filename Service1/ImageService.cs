using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Service
{
    public class ImageService : IImageService
    {
        public void GetImageByArticleId(int id, Action<byte[]> callback)
        {
            var uri = new Uri(string.Format("http://ifx-services.interfax-aki.ru/WebImage/default.aspx?type=news&id={0}&size=0", id));
            WebClient client = new WebClient();

            client.OpenReadCompleted += (object sender, OpenReadCompletedEventArgs e) =>
            {
                lock (this)
                {
                    byte[] buffer = new byte[e.Result.Length];
                    e.Result.Read(buffer, 0, Convert.ToInt32(e.Result.Length));
                    callback(buffer);
                }
            };

            client.OpenReadAsync(uri);
        }
    }
}
