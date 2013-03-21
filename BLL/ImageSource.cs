using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ImageSource
    {
        IImageService _service;

        public ImageSource(IImageService service)
        {
            _service = service;
        }

        public Uri GetImageByArticleId(int id, int size = 1)
        {
            return size == 0
                       ? new Uri(string.Format("http://services.ifx.ru/WebImage/default.aspx?type=news&id={0}&size=1",
                                               id))
                       : new Uri(
                             string.Format(
                                 "http://services.ifx.ru/WebImage/default.aspx?type=news&id={0}&size=1&x=150&y=100&trans=crop",
                                 id));
        }
    }
}
