using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    public class Register : Request
    {
        public Register()
            : base("/api/user/register", RequestType.POST)
        {

        }

        public void SetParam(string firstName, string lastName, string mail, string pass, string avatar)
        {
            uri = new Uri(url + "?firstname=" + firstName + "&lastname=" + lastName + "&email=" + mail + "&password=" + pass + " &avatar=" + avatar);
        }
    }
}
