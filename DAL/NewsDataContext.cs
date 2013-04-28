using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace DAL
{
    public class NewsDataContext : DataContext
    {
        private static string _connectionString = "Data Source=isostore:/NewsCache.sdf;";

        public NewsDataContext()
            : base(_connectionString)
        { }

        public NewsDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<NewsItem> NewsItem;

        public Table<FreePhotoStory> FreePhotoStory;
    }
}
