using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces
{
    public interface INewsDataSource
    {
        void GetFreeNewsList(DateTime updatemark, Action<List<News>> callback);
        void GetFreeArticleList(DateTime updatemark, Action<List<Article>> callback);
        void GetFreeNewsById(string newsId, Action<News> callback);
    }
}
