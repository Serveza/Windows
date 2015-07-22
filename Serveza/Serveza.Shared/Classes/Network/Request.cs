using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Serveza.Classes.Network
{
    public enum RequestType
    {
        GET,
        POST,
    }
    public class Request
    {
        private string BaseAddress = "http://serveza.kokakiwi.net";
        protected Uri uri;
        protected string url;
        protected HttpWebRequest request;
        private RequestType type;
        public Request(string url, RequestType type)
        {
            this.url = BaseAddress + url;
            this.type = type;
        }

        public JObject ExecRequest()
        {
            JObject returnedObject = null;
            try
            {
                Debug.WriteLine("URI : " + uri.ToString());
                request = WebRequest.CreateHttp(uri);
                if (type == RequestType.GET)
                    request.Method = "GET";
                else if (type == RequestType.POST)
                    request.Method = "POST";
                WebResponse response = request.GetResponseAsync().Result;
                Debug.WriteLine("ok");
                HttpWebResponse httpWebResponse = (HttpWebResponse)response;
                if (httpWebResponse.StatusDescription == "OK")
                {
                    Stream stream = response.GetResponseStream();
                    string rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    returnedObject = JObject.Parse(rawJson);
                    Debug.WriteLine(returnedObject);
                    return returnedObject;
                }
                else
                {
                    Utils.Utils.DisplayStatusCode(httpWebResponse.StatusCode);
                    return returnedObject;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(url + "  " + ex);
                return null;
            }
            return returnedObject;
        }
    }
}
