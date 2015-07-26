using Newtonsoft.Json.Linq;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Serveza.Classes.PubList
{
    public class PubList
    {
        public List<Pub> list;

        private PubListViewModel _plvm;
        public PubListViewModel plvm
        {
            get { return _plvm; }
            set
            {
                _plvm = value;
                if (value != null)
                    _plvm.list = new ObservableCollection<Pub>(list);
            }
        }

        public PubList()
        {
            list = new List<Pub>();
            this.plvm = null;
        }
        public PubList(PubListViewModel plvm)
        {
            list = new List<Pub>();
            this.plvm = plvm;
        }

        public bool Load(JObject obj)
        {
            try
            {
                this.Clear();
                JArray array = obj["bars"].ToObject<JArray>();
                string[] posStringSplit;
                string posString;
                string name;
                string url;
                int id = 0;
                Pub pub;
                foreach (JObject bar in array)
                {
                    name = bar["name"].ToObject<string>();
                    posString = bar["position"].ToObject<string>();
                    url = bar["url"].ToObject<string>();
                    id = bar["id"].ToObject<int>();

                    posStringSplit = posString.Split(' ', ',');
                    pub = new Pub(name, Convert.ToDouble(posStringSplit[0]), Convert.ToDouble(posStringSplit[2]), url, id);
                    this.Add(pub);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return true;
        }
        public async void Add(Pub newPub)
        {
            Serveza.Classes.Network.GetBarInfo infoBar = new Classes.Network.GetBarInfo();
            infoBar.SetParam(newPub);
            list.Add(newPub);
            if (plvm != null)
                plvm.list = new ObservableCollection<Pub>(list);
            try
            {
                newPub.getInfo(await infoBar.GetJsonAsync());
                newPub.getEvent(await infoBar.GetJsonAsync());
                newPub.getAddress();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PubList.Add : " + ex.ToString());
            }

        }

        public void Clear()
        {
            list.Clear();
            if (plvm != null)
                plvm.list = new ObservableCollection<Pub>(list);
        }

        public BeerList.BeerList getBeerList()
        {
            BeerList.BeerList Beerlist = new BeerList.BeerList();

            foreach (Pub p in list)
            {
                foreach (Beer b in p.beerList.list)
                {
                    Beerlist.Add(b);
                }
            }
            return Beerlist;
        }

        public PubList SearchBarByBeer(BeerList.BeerList beerListTosearch)
        {
            PubList pubList = new PubList();

            pubList.list = (from pub in list where (pub.HasThisBeers(beerListTosearch)) select pub).ToList();

            return pubList;

        }

        public void SetFav(PubList pubList)
        {
            foreach (Pub pT in pubList.list)
            {
                pT.isFav = false;
            }

            foreach (Pub p in pubList.list)
            {
                foreach (Pub ptwo in list)
                {
                    if (p.id == ptwo.id)
                    {
                        ptwo.isFav = true;
                    }
                }
            }
        }

        public void Remove(Pub pub)
        {
            list.Remove(pub);
            if (plvm != null)
                plvm.list = new ObservableCollection<Pub>(list);
        }

        public void SetFav()
        {
            foreach (Pub p in list)
            {
                p.isFav = true;
            }
        }
    }
}
