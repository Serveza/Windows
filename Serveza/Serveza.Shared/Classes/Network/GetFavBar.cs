using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class GetFavBar : Request
    {
        public GetFavBar() : base("/api/user/favorites/bars", RequestType.GET) { }

        public void SetParam(string token)
        {
            url += "?api_token=" + token;
            uri = new Uri(url);
        }
    }
}
