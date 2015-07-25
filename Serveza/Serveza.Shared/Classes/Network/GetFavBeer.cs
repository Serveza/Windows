using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class GetFavBeer : Request
    {
        public GetFavBeer() : base("/api/user/favorites/beers", RequestType.GET) { }

        public void SetParam(string token)
        {
            url += "?api_token=" + token;
            uri = new Uri(url);
        }
    }
}
