using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Services
{
    public class IFXServiceClient
    {
        protected delegate TResult ProcessResponse<out TResult>(string response);

        public void FreeNewsListAsync(Action<string> reply)
        {
            string reqPattern = "<Envelope><Body><fnlreq xmlns=\"http://ifx.ru/IFX3WebService\"><fnlcnt>{0}</fnlcnt><fnlsi i:nil=\"true\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" /></fnlreq></Body></Envelope>";
            string soapRequest = string.Format(reqPattern, 10);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://ifx-services.interfax-aki.ru/ifxservice.svc");



            string result = WebServiceCall(delegate(string response)
            {
               return response;

            }, request, "FreeNewsList", soapRequest);

            reply(result);
        }

        /// <summary>
        /// Обработка ответов от веб сервиса. Проверка параметров и обработка исключений
        /// </summary>
        /// <typeparam name="TResult">Тип запроса</typeparam>
        /// <param name="action">Действие на веб сервисе</param>
        /// <param name="request">Запрос</param>
        /// <param name="methodName">Имя метода</param>
        /// <param name="soapRequestBody">Тело soap запроса</param>
        /// <param name="clearCookiesOnError">Стирать сессионную куку при ошибке</param>
        /// <returns></returns>
        protected TResult WebServiceCall<TResult>(
            ProcessResponse<TResult> action,
            HttpWebRequest request,
            string methodName,
            string soapRequestBody,
            bool clearCookiesOnError = true)
        {
            TResult result;

            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request");
                }
                if (string.IsNullOrEmpty(methodName))
                {
                    throw new ArgumentNullException("methodName");
                }
                if (string.IsNullOrEmpty(soapRequestBody))
                {
                    throw new ArgumentNullException("soapRequestBody");
                }


                string actionParam = string.Format("action=\"{0}\"", string.Format("http://ifx.ru/IFX3WebService/IIFXService/{0}", methodName));
                request.ContentType = string.Format("application/soap+xml;charset=utf-8; {0}", actionParam);
                request.Method = "POST";
                //System.Net.ServicePointManager.Expect100Continue = false;

                IAsyncResult asyncResult = request.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                // get the response from the completed web request.
                string soapResult;
                using (WebResponse webResponse = request.EndGetResponse(asyncResult))
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                    return action(soapResult);
                }
            }
            catch (WebException)
            {
                /*if (clearCookiesOnError)
                {
                    this.sessionCookies = null;
                }

                string exceptionContent = GetWebExceptionContent(wEx);
                WebServiceException webSrvEx = ProcessWebException(exceptionContent);

                if (webSrvEx != null)
                {
                    throw webSrvEx;
                }

                throw new Exception();*/

                throw;
            }
        }
    }
}
