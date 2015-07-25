using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    class GetBeerInfo : Request
    {
        public GetBeerInfo() : base("/api/beers/", RequestType.GET) { }

        public void SetParam(int id)
        {
            url += id.ToString();
            uri = new Uri(url);
        }
    }
}
