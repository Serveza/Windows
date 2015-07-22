using Serveza.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class GetBarInfo : Request
    {
        public GetBarInfo()
            : base("", RequestType.GET)
        {

        }

        public void SetParam(Pub pub)
        {
            uri = new Uri(url + pub.url);
        }
    }
}
