using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Common
{
    public class Article
    {
        public string Id { get; set; }

        public string Headline { get; set; }

        public DateTime CreateDate { get; set; }

        public string Content { get; set; }

        public Uri Image { get; set; }
    }
}
