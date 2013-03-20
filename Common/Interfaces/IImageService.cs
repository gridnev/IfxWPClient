using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces
{
    public interface IImageService
    {
        void GetImageByArticleId(int id, Action<byte[]> callback);
    }
}
