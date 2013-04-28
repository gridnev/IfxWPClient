using Common;
using Common.Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace BLL
{
    public class NewsSource : INewsDataSource
    {
        IIFXWebService _client;
        BLL.ImageSource _imageSource;

        public NewsSource(IIFXWebService client, BLL.ImageSource imageSource)
        {
            _client = client;
            _imageSource = imageSource;
        }

        public void GetFreeNewsList(DateTime updatemark, Action<List<NewsItem>> callback)
        {
            _client.FreeNewsListCompleted += (object sender, MyEventArgs e) => {
                var t = XElement.Parse(e.Result);
                var newsList =
                    (from node in t.Descendants()
                     where node.Name.LocalName == "c_freenewsupdate"
                     select new NewsItem()
                     {
                         Id = GetElementValue(node, "id"),
                         Headline = GetElementValue(node, "t"),
                         CreateDate = DateTime.Parse(GetElementValue(node, "dc")),
                         NewsItemType = NewsItemType.News
                     }).ToList();

                callback(newsList);
            };

            _client.FreeNewsListAsync(updatemark);
        }

        public void GetFreeNewsById(string newsId, Action<NewsItem> callback)
        {
            _client.GetNewsByIdCompleted += (object sender, MyEventArgs e) =>
            {
                var t = XElement.Parse(e.Result);
                NewsItem news =
                    (from node in t.Descendants()
                     where node.Name.LocalName == "fnini"
                     select new NewsItem()
                     {
                         Id = GetElementValue(node, "id"),
                         Headline = GetElementValue(node, "t"),
                         Content = ConvertExtendedAscii(GetElementValue(node, "ft")),
                         CreateDate = DateTime.Parse(GetElementValue(node, "dc"))
                     }).SingleOrDefault();
                callback(news);
            };

            _client.GetNewsByIdAsync(newsId);
        }

        public void GetFreeArticleList(DateTime updatemark, Action<List<NewsItem>> callback)
        {
            _client.FreeArticleListCompleted += (object sender, MyEventArgs e) =>
            {
                var t = XElement.Parse(e.Result);
                var articleList =
                    (from node in t.Descendants()
                     where node.Name.LocalName == "c_freenewsupdate"
                     select new NewsItem()
                     {
                         Id = GetElementValue(node, "id"),
                         Headline = GetElementValue(node, "t"),
                         CreateDate = DateTime.Parse(GetElementValue(node, "dc")),
                         ImageUrl = _imageSource.GetImageByArticleId(Convert.ToInt32(GetElementValue(node, "id"))),
                         NewsItemType = NewsItemType.Article
                     }).ToList();

                callback(articleList);
            };

            _client.FreeArticleListAsync(updatemark);
        }

        public void GetFreePhotoStoryList(DateTime updatemark, Action<List<FreePhotoStory>> callback)
        {
            _client.FreePhotoStoryListCompleted += (object sender, MyEventArgs e) =>
            {
                var t = XElement.Parse(e.Result);
                var photoStoryList =
                    (from node in t.Descendants()
                     where node.Name.LocalName == "c_shortphotoupdate"
                     select new FreePhotoStory()
                     {
                         Id = GetElementValue(node, "id"),
                         Headline = GetElementValue(node, "t"),
                         CreateDate = DateTime.Parse(GetElementValue(node, "dc")),
                         ImageUrl = _imageSource.GetMainImageByPhotoStoryId(Convert.ToInt32(GetElementValue(node, "id"))),
                     }).ToList();

                callback(photoStoryList);
            };

            _client.FreePhotoStoryListAsync(updatemark);
        }

        private string GetElementValue(XElement element, string name)
        {
            var elem = element.Descendants().SingleOrDefault(n=>n.Name.LocalName == name);
            return elem == null ? null : elem.Value;
        }

        private static string ConvertExtendedAscii(string content)
        {
            var retVal = new StringBuilder();
            var s = content.ToCharArray();

            foreach (char c in s)
            {
                if (Convert.ToInt32(c) > 127)
                    retVal.AppendFormat("&#{0};", Convert.ToInt32(c));
                else
                    retVal.Append(c);
            }

            return retVal.ToString();
        }
    }
}
