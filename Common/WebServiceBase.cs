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
    public class WebServiceBase
    {
        public void WebServiceCall(string serviceUrl, string methodName, string soapRequestBody, EventHandler<MyEventArgs> WebServiceCallCompleted)
        {
            WebClient client = new WebClient();

            string actionParam = string.Format("action=\"{0}\"", string.Format("http://ifx.ru/IFX3WebService/IIFXService/{0}", methodName));
            string contentType = string.Format("application/soap+xml;charset=utf-8; {0}", actionParam);

            client.Headers[HttpRequestHeader.ContentType] = contentType;

            client.UploadStringCompleted += (object sender, UploadStringCompletedEventArgs e) =>
            {
                WebServiceCallCompleted(this, new MyEventArgs() { Result = e.Result});
            };

            client.UploadStringAsync(new Uri(serviceUrl), soapRequestBody);
        }
    }
}
