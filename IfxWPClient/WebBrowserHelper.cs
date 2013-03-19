using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace IfxWPClient
{
    public static class WebBrowserHelper
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html", typeof(string), typeof(WebBrowserHelper), new PropertyMetadata(OnHtmlChanged));

        public static string GetHtml(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = d as WebBrowser;

            if (browser == null)
                return;

            var html = ConvertExtendedAscii(e.NewValue.ToString());

            browser.NavigateToString(string.Format("<html><head><body style=\"background-color: #242834; color: white;\">{0}</body></html>", html));
            browser.LoadCompleted += (sender, args) =>
            {
                browser.Opacity = 1;
            };
        }


        private static string ConvertExtendedAscii(string html)
        {
            var retVal = "";
            var s = html.ToCharArray();

            foreach (char c in s)
            {
                if (Convert.ToInt32(c) > 127)
                    retVal += "&#" + Convert.ToInt32(c) + ";";
                else
                    retVal += c;
            }

            return retVal;
        }
    }
}
