using Serveza.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes
{
    public class Collection
    {
        public bool isBeer;
        public Settings.Settings settings;
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

        public BeerList.BeerList NeerBeer;
        public PubList.PubList resultPubList;
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
            NeerBeer = new BeerList.BeerList();
            resultPubList = new PubList.PubList();
            User = new User.User();
            settings = new Settings.Settings();
        }

        public void Init()
        {
            
        }
    }
}
