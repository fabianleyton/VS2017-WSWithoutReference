using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace testSoapService.ClassWS
{
    public static class WSSoapRequest
    {
        public static string Peticion(string url, string action, string SOAPEnvelope, string contentType, string method, string userName, string password)
        {
            var envelopeXml = CreateEnvelope(SOAPEnvelope);
            var request = CreateRequest(url, action, contentType, method, userName, password);

            using (Stream stream = request.GetRequestStream())
                envelopeXml.Save(stream);

            using (var strWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(strWriter))
                {
                    envelopeXml.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }

            var asyncResult = request.BeginGetResponse(null, null);

            var result = asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));

            if (!result)
                return null;
            else
                using (var webResponse = request.EndGetResponse(asyncResult))
                {
                    string soapResult;
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return null;
                    using (var reader = new StreamReader(responseStream))
                        soapResult = reader.ReadToEnd();
                    return soapResult;
                }
        }
        private static HttpWebRequest CreateRequest(string url, string action, string contentType = "application/soap+xml;charset=\"utf-8\"", string method = "POST", string userName = null, string password = null)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("action", action);
            webRequest.ContentType = contentType;
            webRequest.Method = method;
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
                webRequest.Credentials = new NetworkCredential() { UserName = userName, Password = password };
            return webRequest;
        }
        
        private static XmlDocument CreateEnvelope(string envelopeObj)
        {
            var soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml(envelopeObj);
            return soapEnvelope;
        }
    }
}