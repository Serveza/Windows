using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using Serveza.Model;

namespace Serveza.Classes.Network
{
    public class NetWork
    {
        public Uri BaseAddress;
        public string token;
        public NetWork()
        {
            BaseAddress = new Uri("http://192.168.1.32:5000");
        }

        public BeerList.BeerList getBeerList()
        {
            return new BeerList.BeerList();
        }

        public BeerList.BeerList getFavoriteBeer()
        {
            return new BeerList.BeerList();
        }

        public PubList.PubList getPubList()
        {
            return new PubList.PubList();
        }

        public EventList.EventList getEventList()
        {
            return new EventList.EventList();
        }

        public CommentList.CommentList getBeerComment()
        {
            return new CommentList.CommentList();
        }

        public CommentList.CommentList getPubComment()
        {
            return new CommentList.CommentList();
        }

        public bool CreateAccount(string firstName, string lastName, string mail, string pass)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(BaseAddress + "api/user/register?firstname=" + firstName + "&lastname=" + lastName + "&email=" + mail + "&password=" + pass);
                request.Method = "POST";
                WebResponse response = request.GetResponseAsync().Result;
                Debug.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream stream = response.GetResponseStream();

                Debug.WriteLine("Response stream received.");
                string rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();

                JObject obj = JObject.Parse(rawJson);

                Debug.WriteLine(obj);

                if (obj["email"].ToObject<string>() == mail)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

       
    }
}
