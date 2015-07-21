using Serveza.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes
{
    public class Collection
    {
        public bool isBeer;
        public BeerList.BeerList MyBeerList { get; set; }
        public BeerList.BeerList SearchBeerList { get; set; }
        public PubList.PubList ResultPubList { get; set; }
        public PubList.PubList MyPubList { get; set; }
        public PubList.PubList NeerPubList { get; set; }
        public EventList.EventList MyEventList { get; set; }
        public Location.LocationCore LocationCore { get; set; }
        public Network.NetWork netWork { get; set; }
        public User.User User { get; set; }

        private Beer _beerToDisplay;
        private Pub _pubToDisplay;
        public Beer BeerToDisplay
        {
            get
            {
                return _beerToDisplay;
            }
            set
            {
                _beerToDisplay = value;
                isBeer = true;
            }
        }
        public Pub PubToDisplay
        {
            get { return _pubToDisplay; }
            set
            {
                _pubToDisplay = value;
                isBeer = false;
            }
        }
        public Collection()
        {
            MyBeerList = new BeerList.BeerList();
            MyEventList = new EventList.EventList();
            MyPubList = new PubList.PubList();
            NeerPubList = new PubList.PubList();
            SearchBeerList = new BeerList.BeerList();
            LocationCore = new Location.LocationCore();
            ResultPubList = new PubList.PubList();
            netWork = new Network.NetWork();
            User = new User.User();
        }

        public void Init()
        {
            MyBeerList.Add(new Beer("Guiness", 4.2, "Guiness"));
            MyBeerList.Add(new Beer("Leff", 5, "Leff"));
            MyBeerList.Add(new Beer("Rince Cochon", 8.5, "Rince Cochon"));
            MyBeerList.Add(new Beer("Cuvee des Trols", 7, "Cuvee des Trols"));

           
            MyPubList.Add(new Pub("Imaginaire", 50.6404403, 3.064249));
            MyPubList.Add(new Pub("Capsule", 50.6405435, 3.0601101));
            MyPubList.Add(new Pub("Chopp'ing", 50.6393988, 3.058493));
            MyPubList.Add(new Pub("O'Scotland", 50.6308288, 3.0552842));

        }
    }
}
