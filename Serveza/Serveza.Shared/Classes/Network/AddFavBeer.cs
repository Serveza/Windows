using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.Network
{
    public class AddFavBeer : Request
    {
        public AddFavBeer()
            : base("/api/user/favorites/beers", RequestType.POST)
        {

        }

        public void SetParam(string token, int id)
        {
            content = new Windows.Web.Http.HttpStringContent("{ \"api_token\":\"" + token +
                                                              "\", \"beer\":" + id.ToString() +
                                                              "}",
                                                               Windows.Storage.Streams.UnicodeEncoding.Utf8,
                                                              "application/json");
            Debug.WriteLine(content);
            uri = new Uri(url);
        }
    }
}
