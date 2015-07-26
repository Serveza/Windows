using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Serveza.Classes.Network
{
    public enum RequestType
    {
        GET,
        POST,
        DELETE,
    }
    public class Request
    {
        private string BaseAddress = "http://10.11.45.251:5000";
        //private string BaseAddress = "http://serveza.kokakiwi.net";
        protected Uri uri;
        protected string url;
        protected HttpWebRequest request;
        protected HttpStringContent content;
        private RequestType type;
        public Request(string url, RequestType type)
        {
            this.url = BaseAddress + url;
            this.type = type;
        }

        public async Task<JObject> GetJsonAsync()
        {
            using (var client = new HttpClient())
            {
                string jsonString = "";
                if (type == RequestType.GET)
                    jsonString = await client.GetStringAsync(uri);
                else if (type == RequestType.POST)
                {
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    Debug.WriteLine("response : " + response.ToString());
                    jsonString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(jsonString);
                }
                else if (type == RequestType.DELETE)
                {
                    HttpResponseMessage response = await client.DeleteAsync(uri);
                    Debug.WriteLine("response : " + response.StatusCode.ToString());
                    jsonString = await response.Content.ReadAsStringAsync();
                }
                return JObject.Parse(jsonString);
            }
        }
    }
}
