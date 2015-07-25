using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.Network
{
    public class Register : Request
    {
        public Register()
            : base("/api/user/register", RequestType.POST)
        {

        }

        public void SetParam(string firstName, string lastName, string mail, string pass, string avatar)
        {

            content = new Windows.Web.Http.HttpStringContent("{ \"firstName\":\"" + firstName +
                                                               "\", \"lastname\":\"" + lastName +
                                                               "\", \"email\":\"" + mail +
                                                               "\", \"password\":\"" + pass +
                                                               "\", \"avatar\":\"" + avatar +
                                                               "\"}",
                                                                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                                                               "application/json");
            Debug.WriteLine(content);
            uri = new Uri(url);
        }
    }
}
