﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.User
{
    public class User
    {
        private string _firstName;
        private string _lastName;

        public string imageUrl { get; set; }
        public string firstName { get { return _firstName; } set { _firstName = value; } }
        public string lastName { get { return _lastName; } set { _lastName = value; } }
        public string name
        {
            get
            {
                return _firstName + " " + _lastName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string mail { get; set; }

        public User()
        {

        }
        public bool Load(JObject obj)
        {
            try
            {
                firstName = obj["firstname"].ToObject<string>();
                lastName = obj["lastname"].ToObject<string>();
                imageUrl = obj["avatar"].ToObject<string>();
                App.Core.netWork.token = obj["api_token"].ToObject<string>();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

    }
}
