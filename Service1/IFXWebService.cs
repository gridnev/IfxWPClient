using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace Services
{
    public class IFXWebService: WebServiceBase, IIFXWebService
    {
        const string _serviceURL = "http://services.ifx.ru/ifxservice.svc";

        public event EventHandler<MyEventArgs> FreeNewsListCompleted;
        public void FreeNewsListAsync(DateTime updatemark)
        {
            string reqPattern = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body><fnulreq xmlns=\"http://ifx.ru/IFX3WebService\"><fnuldm>{0}</fnuldm></fnulreq></s:Body></s:Envelope>";
            string soapRequest = string.Format(reqPattern, updatemark.ToString("yyyy-MM-ddThh:mm:ss"));

            WebServiceCall(_serviceURL, "NewsFreeNewsUpdateList", soapRequest, FreeNewsListCompleted);
        }

        public event EventHandler<MyEventArgs> GetNewsByIdCompleted;
        public void GetNewsByIdAsync(string newsId)
        {
            string reqPattern = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body><fnireq xmlns=\"http://ifx.ru/IFX3WebService\"><fniid>{0}</fniid></fnireq></s:Body></s:Envelope>";
            string soapRequest = string.Format(reqPattern, newsId);

            WebServiceCall(_serviceURL, "FreeNewsItem", soapRequest, GetNewsByIdCompleted);
        }
    }
}
