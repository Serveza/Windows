using Newtonsoft.Json.Linq;
using Serveza.Classes.BeerList;
using Serveza.Classes.CommentList;
using Serveza.Classes.EventList;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using System.Linq;
using System.Diagnostics;

namespace Serveza.Model
{
    public class Pub
    {
        //private long id;
        private string _name;
        private double _latitude;
        private double _longitude;

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
            set
            {
                _latitude = value.Position.Latitude;
                _longitude = value.Position.Longitude;
            }
        }
        public BeerList beerList { get; set; }
        public EventList eventList { get; set; }

        public CommentList commentList { get; set; }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public double longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        public string url { get; private set; }
        public double dist
        {
            get
            {
                //var cor = new Geocoordinate(longitude, latitude);
                //return cor.GetDistanceTo(new Geocoordinate, latitude));
                return 0.1;
            }
        }

        public Pub(string name, double clongitude, double clatitude, string url = "", int id = 0)
        {
            _name = name;
            this.url = url;
            this.id = id;

            beerList = new BeerList();
            
            commentList = new CommentList();

            eventList = new EventList();

            _longitude = clatitude;
            _latitude = clongitude;
        }


        public Pub(string name)
        {
            url = name;
            beerList = new BeerList();
            eventList = new EventList();
            commentList = new CommentList();
            //   _longitude = clongitude;
            // _latitude = clatitude;
        }

        public Pub()
        {
           
        }



        public async void getAddress()
        {
            address = await App.Core.LocationCore.GetPubAdress(this);
        }

        public bool getInfo(JObject obj)
        {
            if (obj != null)
            {
                JObject objBar = obj["bar"].ToObject<JObject>();

                beerList.Load(objBar["carte"].ToObject<JArray>());
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return beerList.ToString();
        }


        public string address { get; set; }


        public int id { get; private set; }

        public void getEvent(JObject jObject)
        {

        }

        public bool HasThisBeers(BeerList beerListToSearch)
        {

            foreach (Beer b in beerListToSearch.list)
            {
                var query = (from beer in beerList.list where (b.id == beer.id) select beer).ToList();
                if (query.Count == 0)
                    return false;
            }
            return true;
        }

        public void LoadEvent(JObject jobj)
        {
            try
            {
                eventList.Clear();
                JArray array = jobj["notifications"].ToObject<JArray>();
                Event tmp;
                foreach (JObject obj in array)
                {
                    tmp = new Event();
                    tmp.Load(obj);
                    eventList.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Load(JObject obj, Event eve)
        {
            try
            {
                JObject objBar = obj["bar"].ToObject<JObject>();
                name = objBar["name"].ToObject<string>();
                address = objBar["address"].ToObject<string>();
                id = objBar["id"].ToObject<int>();

                string[] loc = objBar["position"].ToObject<string>().Split(',', ' ');
                _latitude = Convert.ToDouble(loc[0]);
                _longitude = Convert.ToDouble(loc[2]);
                Debug.WriteLine(obj);
                eve.setCoor(_latitude, _longitude, address);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
