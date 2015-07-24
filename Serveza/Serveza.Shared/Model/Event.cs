using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Model
{
    public class Event
    {
        private string toto;

        private DateTime _startTime;
        private DateTime _endTime;

        public string startTime
        {
            get { return _startTime.ToString("MM-dd HH:mm"); }
            set { toto = value; }
        }

        public string endTime
        {
            get
            {
                return " " + _startTime.ToString("MM-dd HH:mm");
            }
            set { toto = value; }
        }
        public string name { get; private set; }
        public string desciption { get; private set; }

        public Event(DateTime start, DateTime end, string name, string description)
        {
            _startTime = start;
            _endTime = end;
            this.name = name;
            this.desciption = desciption;
        }

        public Event()
        {
            
        }



        public void Load(Newtonsoft.Json.Linq.JObject notif)
        {
            try
            {
                name = notif["name"].ToObject<string>();
                string desc = notif["descriotion"].ToObject<string>();
                desciption = (desc == null ? "No description" : desc);
                
                _startTime = DateTime.Now;
                _endTime = DateTime.Now;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
