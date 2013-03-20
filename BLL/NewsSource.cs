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

        public void GetFreeNewsList(DateTime updatemark, Action<List<News>> callback)
        {
            _client.FreeNewsListCompleted += (object sender, MyEventArgs e) => {
                var t = XElement.Parse(e.Result);
                var newsList =
                    (from node in t.Descendants()
                     where node.Name.LocalName == "c_freenewsupdate"
                     select new News()
                     {
                         Id = GetElementValue(node, "id"),
                         Headline = GetElementValue(node, "t"),
                         CreateDate = DateTime.Parse(GetElementValue(node, "dc"))
                     }).ToList();

                callback(newsList);
            };

            _client.FreeNewsListAsync(updatemark);
        }

        public void GetFreeNewsById(string newsId, Action<News> callback)
        {
            _client.GetNewsByIdCompleted += (object sender, MyEventArgs e) =>
            {
                var t = XElement.Parse(e.Result);
                var news =
                    (from node in t.Descendants()
                     where node.Name.LocalName == "fnini"
                     select new News()
                     {
                         Id = GetElementValue(node, "id"),
                         Headline = GetElementValue(node, "t"),
                         Content = GetElementValue(node, "ft"),
                         CreateDate = DateTime.Parse(GetElementValue(node, "dc"))
                     }).SingleOrDefault<News>();

                callback(news);
            };

            _client.GetNewsByIdAsync(newsId);
        }

        public void GetFreeArticleList(DateTime updatemark, Action<List<Article>> callback)
        {
            _client.FreeArticleListCompleted += (object sender, MyEventArgs e) =>
            {
                var t = XElement.Parse(e.Result);
                var articleList =
                    (from node in t.Descendants()
                     where node.Name.LocalName == "c_freenewsupdate"
                     select new Article()
                     {
                         Id = GetElementValue(node, "id"),
                         Headline = GetElementValue(node, "t"),
                         CreateDate = DateTime.Parse(GetElementValue(node, "dc")),
                         Image = _imageSource.GetImageByArticleId(Convert.ToInt32(GetElementValue(node, "id")))
                     }).ToList();

                callback(articleList);
            };

            _client.FreeArticleListAsync(updatemark);
        }

        private string GetElementValue(XElement element, string name)
        {
            var elem = element.Descendants().SingleOrDefault(n=>n.Name.LocalName == name);
            return elem == null ? null : elem.Value;
        }
    }
}
