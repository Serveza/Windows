using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class GetBarComment : Request
    {
        public GetBarComment()
            : base("/api/bars", RequestType.GET)
        {

        }

        public void SetParam(int id)
        {
            url += "/" + id.ToString() + "/comments";
            uri = new Uri(url);
        }
    }
}
