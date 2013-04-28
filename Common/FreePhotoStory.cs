using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Common
{
    [Table]
    public class FreePhotoStory
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }

        [Column]
        public string Headline { get; set; }

        [Column]
        public DateTime CreateDate { get; set; }

        [Column(DbType = "nvarchar(255)")]
        public string ImageUrl { get; set; }
    }
}
