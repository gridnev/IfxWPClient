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

        public string GetImageByArticleId(int id, int size = 1)
        {
            return size == 0
                       ? string.Format("http://services.ifx.ru/WebImage/default.aspx?type=news&id={0}&size=1",
                                               id)
                       : string.Format(
                                 "http://services.ifx.ru/WebImage/default.aspx?type=news&id={0}&size=1&x=150&y=100&trans=crop",
                                 id);
        }

        public string GetMainImageByPhotoStoryId(int id, int size = 1)
        {
            return size == 0
                       ? string.Format("http://services.ifx.ru/WebImage/default.aspx?type=photostory&id={0}&size=1",
                                               id)
                       : string.Format(
                                 "http://services.ifx.ru/WebImage/default.aspx?type=photostory&id={0}&size=1",
                                 id);
        }
    }
}
