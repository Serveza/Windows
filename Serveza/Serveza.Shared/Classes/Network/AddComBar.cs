using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.Network
{
    public class AddComBar : Request
    {
        public AddComBar() : base("/api/bars/", RequestType.POST)
        {

        }

        public void SetParam(string token, int id, string comment, int note)
        {
            content = new Windows.Web.Http.HttpStringContent("{ \"api_token\":\"" + token +
                                                              "\", \"score\":" + note.ToString() +
                                                              ", \"comment\":\"" + comment +
                                                              "\"}",
                                                               Windows.Storage.Streams.UnicodeEncoding.Utf8,
                                                              "application/json");

            url += id.ToString() + "/comments";
            uri = new Uri(url);
            Debug.WriteLine(uri.ToString() +  " -> " +content.ToString()); 
            
        }
    }
}
