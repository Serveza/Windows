using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class GetComBeer : Request
    {
        public GetComBeer() 
            : base("/api/beers", RequestType.GET) 
        {
        }

        public void SetParam(int id)
        {
            url += "/" + id.ToString() + "/comments";
            uri = new Uri(url);
        }
    }
}
