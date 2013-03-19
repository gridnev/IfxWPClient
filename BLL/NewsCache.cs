using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class NewsCache:INewsDataSource
    {
        List<News> _newsCache;
        NewsSource _newsSource;

        public NewsCache(NewsSource newsSource)
        {
            _newsCache = new List<News>();
            _newsSource = newsSource;
        }

        private void UpdateCache(DateTime updatemark)
        {
            _newsSource.GetFreeNewsList(updatemark, c => _newsCache.AddRange(c));
        }

        public void GetFreeNewsList(DateTime updatemark, Action<List<News>> callback)
        {
            if (!_newsCache.Any())
            {
                _newsSource.GetFreeNewsList(updatemark, c =>
                {
                    _newsCache.AddRange(c);
                    callback(_newsCache);
                });
            }
            else 
            {
                callback(_newsCache);
            }
        }

        public void GetFreeNewsById(string newsId, Action<News> callback)
        {
            var news = _newsCache.SingleOrDefault(n=>n.Id == newsId);
            if (news != null && news.Content != null)
                callback(news);
            else
            {
                 _newsSource.GetFreeNewsById(newsId, r => {
                     if (!_newsCache.Any(n => n.Id == r.Id))
                         _newsCache.Add(r);
                     else
                         _newsCache.SingleOrDefault(n => n.Id == r.Id).Content = r.Content;

                     callback(r);
                });
            }

           
        }
    }
}
