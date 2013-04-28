using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Common.Interfaces
{
    public interface INewsRepository
    {
        List<NewsItem> GetNewsList(DateTime updatemark);
        List<NewsItem> GetAllNewsItem();
        NewsItem GetFreeNewsById(string id);
        void UpdateNews(NewsItem news);
        List<NewsItem> GetArticlesList(DateTime updatemark);

        void SaveNewsList(List<NewsItem> newsList);
        void RemoveNews(NewsItem item);
        void RemoveNewsList(List<NewsItem> items);

    }
}
