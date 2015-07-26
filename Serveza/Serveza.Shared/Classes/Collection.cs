using Serveza.Classes.Network;
using Serveza.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

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
        public Event EventToDisplay;
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

        private async void GetUserEvent()
        {
            Debug.WriteLine("getUserEvent");
            App.Core.User.eventList.Clear();
            Classes.Network.GetEvent getEvent = new Classes.Network.GetEvent();
            getEvent.SetParam(App.Core.netWork.token, Classes.Network.EventType.NONE);
            var obj = await getEvent.GetJsonAsync();
            App.Core.User.eventList.LoadEvent(obj);
            Debug.WriteLine("getUserEvent done");
        }

        private async void NeerBars()
        {

            Serveza.Classes.Network.NeerBar request = new Serveza.Classes.Network.NeerBar();
            var geo = await App.Core.LocationCore.GetUserPosition();
            request.setParam(geo, settings.scope);
            var obj = await request.GetJsonAsync();
            NeerPubList.Load(obj);
        }

        public async Task<bool> Connect(string userName, string pass)
        {
            try
            {
                Connection co = new Connection();
                co.setParam(userName, pass);
                var obj = await co.GetJsonAsync();
                Debug.WriteLine(obj);
                if (User.Load(obj))
                {
                    Debug.WriteLine("userConnect");
                    Utils.StorageApplication.SetValue("token", App.Core.netWork.token);

                    Debug.WriteLine("get Neer Bars");
                    Serveza.Classes.Network.NeerBar request = new Serveza.Classes.Network.NeerBar();
                    var geo = await App.Core.LocationCore.GetUserPosition();
                    request.setParam(geo, settings.scope);
                    var objNeerBar = await request.GetJsonAsync();
                    NeerPubList.Load(objNeerBar);
                    
                    Debug.WriteLine("getUserEvent");
                    
                    Classes.Network.GetEvent getEvent = new Classes.Network.GetEvent();
                    getEvent.SetParam(App.Core.netWork.token, Classes.Network.EventType.NONE);
                    var objEvent = await getEvent.GetJsonAsync();
                    App.Core.User.eventList.LoadEvent(objEvent);

                 /*   Debug.WriteLine("GetFavBar");
                    
                    Classes.Network.GetFavBar getfavBar = new GetFavBar();
                    getfavBar.SetParam(netWork.token);
                    var objFavBar = await getfavBar.GetJsonAsync();
                    MyPubList.Load(objFavBar);

                    Debug.WriteLine("GetFavBeer");

                    GetFavBeer getFavBeer = new GetFavBeer();
                    getFavBeer.SetParam(netWork.token);
                    var objFavBeer = await getFavBeer.GetJsonAsync();
                   // MyBeerList.Load(objFavBeer);
                    */
                    Debug.WriteLine("done");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
