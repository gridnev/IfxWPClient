using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Common
{
    [Table]
    public class NewsItem
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }

        [Column]
        public string Headline { get; set; }

        [Column]
        public DateTime CreateDate { get; set; }

        [Column(DbType = "NText", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
        public string Content { get; set; }

        [Column(DbType="nvarchar(255)")]
        public string ImageUrl { get; set; }

        [Column]
        public NewsItemType NewsItemType { get; set; }

        public bool IsObsolete()
        {
            return (this.NewsItemType == NewsItemType.News && this.CreateDate < DateTime.Now.AddHours(-24)) ||
                (this.NewsItemType == NewsItemType.Article && this.CreateDate < DateTime.Now.AddDays(-7));
        }
    }
}
