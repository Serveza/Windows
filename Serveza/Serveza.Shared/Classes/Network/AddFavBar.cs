using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.Network
{
    class AddFavBar : Request
    {
        public AddFavBar() : base("/api/user/favorites/bars", RequestType.POST) { }

        public void SetParam(string token, int id)
        {
            content = new Windows.Web.Http.HttpStringContent("{ \"api_token\":\"" + token +
                                                               "\", \"bar\":" + id.ToString() +
                                                               "}",
                                                                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                                                               "application/json");
            Debug.WriteLine(content);
            uri = new Uri(url);
        }
    }
}
