using Serveza.Classes.BeerList;
using Serveza.Classes.CommentList;
using Serveza.Classes.EventList;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;

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
        public string url { get; set; }
        public double dist
        {
            get { return 0.1; }
        }

        public Pub(string name, double clongitude, double clatitude, string url = "")
        {
            _name = name;
            this.url = url;
            beerList = new BeerList();

            beerList.Add(new Beer("Guiness", 4.2, "Guiness"));
            beerList.Add(new Beer("Leff", 5, "Leff"));
            beerList.Add(new Beer("Rince Cochon", 8.5, "Rince Cochon"));
            beerList.Add(new Beer("Cuvee des Trols", 7, "Cuvee des Trols"));

            eventList = new EventList();

            eventList.Add(new Event(DateTime.Now, DateTime.Now, "NewEvent", "Just a new Event"));
            eventList.Add(new Event(DateTime.Now, DateTime.Now, "NewEvent", "Just a new Event"));
            eventList.Add(new Event(DateTime.Now, DateTime.Now, "NewEvent", "Just a new Event"));
            eventList.Add(new Event(DateTime.Now, DateTime.Now, "NewEvent", "Just a new Event"));

            _longitude = clatitude;
            _latitude = clongitude;
        }


        public Pub(string name)
        {
            _name = name;
            beerList = new BeerList();
            beerList.Add(new Beer("Guiness", 4.2, "Guiness"));
            beerList.Add(new Beer("Leff", 5, "Leff"));
            beerList.Add(new Beer("Rince Cochon", 8.5, "Rince Cochon"));
            beerList.Add(new Beer("Cuvee des Trols", 7, "Cuvee des Trols"));


            //   _longitude = clongitude;
            // _latitude = clatitude;
        }



        public async void getInfo()
        {
            address = await App.Core.LocationCore.GetPubAdress(this);
        }

        public override string ToString()
        {
            return beerList.ToString();
        }


        public string address { get; set; }
    }
}
