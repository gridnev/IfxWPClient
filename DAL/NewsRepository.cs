using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class NewsRepository: INewsRepository
    {
        NewsDataContext _ctx;

        public NewsRepository()
        {
            _ctx = new NewsDataContext();

            if (!_ctx.DatabaseExists())
            {
                _ctx.CreateDatabase();
            }
        }

        public List<NewsItem> GetNewsList(DateTime updatemark)
        {
            return _ctx.NewsItem.Where(i => i.NewsItemType == NewsItemType.News && i.CreateDate > updatemark).ToList();
        }

        public List<NewsItem> GetArticlesList(DateTime updatemark)
        {
            return _ctx.NewsItem.Where(i=>i.NewsItemType== NewsItemType.Article && i.CreateDate > updatemark).ToList();
        }

        public NewsItem GetFreeNewsById(string id)
        {
            return _ctx.NewsItem.SingleOrDefault(n => n.Id == id);
        }

        public List<NewsItem> GetAllNewsItem()
        {
            return _ctx.NewsItem.ToList();
        }

        public void UpdateNews(NewsItem news)
        {
            var oldNews = _ctx.NewsItem.SingleOrDefault(n => n.Id == news.Id);
            if (oldNews == null)
                _ctx.NewsItem.InsertOnSubmit(news);

            _ctx.SubmitChanges();
        }

        public void SaveNewsList(List<NewsItem> newsList)
        {
            var newsToSave = newsList.Where(i => !_ctx.NewsItem.Any(c => c.Id == i.Id));
            _ctx.NewsItem.InsertAllOnSubmit(newsToSave);
            _ctx.SubmitChanges();
        }

        public void RemoveNews(NewsItem item)
        {
            _ctx.NewsItem.DeleteOnSubmit(item);
            _ctx.SubmitChanges();
        }

        public void RemoveNewsList(List<NewsItem> items)
        {
            items.ForEach(_ctx.NewsItem.DeleteOnSubmit);
            _ctx.SubmitChanges();
        }
    }
}
