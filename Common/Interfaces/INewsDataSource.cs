using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces
{
    public interface INewsDataSource
    {
        void GetFreeNewsList(DateTime updatemark, Action<List<NewsItem>> callback);
        void GetFreeArticleList(DateTime updatemark, Action<List<NewsItem>> callback);
        void GetFreeNewsById(string newsId, Action<NewsItem> callback);
        void GetFreePhotoStoryList(DateTime updatemark, Action<List<FreePhotoStory>> callback);
    }
}
