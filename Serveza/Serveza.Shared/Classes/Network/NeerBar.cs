using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class NeerBar : Request
    {
        public NeerBar()
            : base("/api/bars", RequestType.GET)
        {
           
        }

        public void setParam(double latitude, double longitude, double dist = 0)
        {
            uri = new Uri(url + "?latitude=" + latitude.ToString() + "&longitude=" + longitude.ToString() + "&range=" + dist.ToString());
        }
    }
}
