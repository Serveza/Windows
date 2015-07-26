using Serveza.Classes.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.Devices.Geolocation;

namespace Serveza.Model
{
    public class Event
    {
        private string toto;

        private double _longitude;
        private double _latitude;
        public string address;

        public Geopoint pos
        {
            get
            {
                return new Geopoint(new BasicGeoposition()
                {
                    Latitude = _latitude,
                    Longitude = _longitude
                });
            }
        }

        public Pub pubAttached { get; private set; }

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
            pubAttached = null;
        }



        public void Load(Newtonsoft.Json.Linq.JObject notif)
        {
            try
            {
                Debug.WriteLine(notif);
                name = notif["name"].ToObject<string>();
                string desc = notif["description"].ToObject<string>();
                desciption = (desc == null ? "No description" : desc);
                string start = notif["start"].ToObject<string>();
                string end = notif["end"].ToObject<string>();
                _startTime = Utils.Utils.StringToTime(start);
                _endTime = Utils.Utils.StringToTime(end);
                if (notif["location"].ToObject<string>() == null)
                {
                    pubAttached = new Pub((notif["bar"].ToObject<string>() == null ? "" : notif["bar"].ToObject<string>()));
                    getInfoPub();
                }
                else
                {
                    pubAttached = null;
                    string[] loc = notif["location"].ToObject<string>().Split(',', ' ');
                    _latitude = Convert.ToDouble(loc[0]);
                    _longitude = Convert.ToDouble(loc[2]);
                    address = null;
                }
              
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void getInfoPub()
        {
            GetBarInfo getBarInfo = new GetBarInfo();
            getBarInfo.SetParam(pubAttached);
            var obj = await getBarInfo.GetJsonAsync();
            pubAttached.Load(obj, this);

        }

        public void setCoor(double latitude, double longitude, string adress)
        {
            this.address = adress;
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}
