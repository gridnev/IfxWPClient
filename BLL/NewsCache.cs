using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class NewsCache: INewsDataSource
    {
        NewsSource _newsSource;
        INewsRepository _newsRepository;

        public NewsCache(NewsSource newsSource, INewsRepository newsRepository)
        {
            _newsSource = newsSource;
            _newsRepository = newsRepository;

            RemoveOldItemsFromCache();
        }

        public void GetFreeNewsList(DateTime updatemark, Action<List<NewsItem>> callback)
        {
            var result = _newsRepository.GetNewsList(updatemark);

            if (result.Any())
                callback(result);
            else
                _newsSource.GetFreeNewsList(updatemark, c =>
                {
                    callback(c);
                    _newsRepository.SaveNewsList(c);
                });
        }

        public void GetFreeNewsById(string newsId, Action<NewsItem> callback)
        {
            var news = _newsRepository.GetFreeNewsById(newsId);
            if (news.Content != null)
                callback(news);
            else
            {
                _newsSource.GetFreeNewsById(newsId, r =>
                {
                    news.Content = r.Content;
                    _newsRepository.UpdateNews(r);
                    callback(r);
                });
            }
        }

        public void GetFreeArticleList(DateTime updatemark, Action<List<NewsItem>> callback)
        {
            var result = _newsRepository.GetArticlesList(updatemark);

            if (result.Any())
                callback(result);
            else
                _newsSource.GetFreeArticleList(updatemark, a =>
                {
                    callback(a);
                    _newsRepository.SaveNewsList(a);
                });
        }

        public void GetFreePhotoStoryList(DateTime updatemark, Action<List<FreePhotoStory>> callback)
        {
            _newsSource.GetFreePhotoStoryList(updatemark, callback);
        }

        private void RemoveOldItemsFromCache()
        {
            var obsoleteNewsItem = _newsRepository.GetAllNewsItem().Where(i => i.IsObsolete()).ToList();
            _newsRepository.RemoveNewsList(obsoleteNewsItem);
        }
    }
}
