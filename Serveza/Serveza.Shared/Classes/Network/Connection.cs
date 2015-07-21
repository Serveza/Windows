using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class Connection : Request
    {
        private string email;
        private string password;

        public Connection()
            : base("/api/user/login", RequestType.POST)
        {

        }

        public void setParam(string email, string password)
        {
            this.email = email;
            this.password = password;
            uri = new Uri(url + "?email=" + email + "&password=" + password);
        }
    }
}
