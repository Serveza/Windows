using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class UnFavBar : Request
    {
        public UnFavBar()
            : base("/api/user/favorites/bars", RequestType.DELETE)
        {

        }

        public void SetParam(string token, int id)
        {

        }
    }
}
