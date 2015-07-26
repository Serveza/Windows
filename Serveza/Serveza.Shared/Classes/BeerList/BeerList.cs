using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Serveza.Classes.BeerList
{
    public class BeerList
    {
        public List<Beer> list;
        private BeerListViewModel _blwm;
        public BeerListViewModel blwm
        {
            get { return _blwm; }
            set
            {
                //   _blwm.detach();
                _blwm = value;
                if (_blwm != null)
                    _blwm.list = new ObservableCollection<Beer>(list);
            }
        }
        public BeerList(BeerListViewModel blwm)
        {
            list = new List<Beer>();
            this.blwm = blwm;
        }

        public BeerList()
        {
            list = new List<Beer>();
            blwm = null;
        }

        public void Add(Beer newBeer)
        {
            var query = (from b in list where (b.id == newBeer.id) select b).ToList();

            if (query.Count == 0)
                list.Add(newBeer);

            if (blwm != null)
                blwm.list = new ObservableCollection<Beer>(list);
        }

        public override string ToString()
        {
            string ret = "";
            int i = 0;
            foreach (Beer b in list)
            {
                if (i < 2)
                    ret += b.name + ", ";
                if (i == 2)
                    ret += b.name;
                i++;
            }
            return ret;
        }

        public ObservableCollection<Beer> Search(string p)
        {
            return new ObservableCollection<Beer>((from beer in list where (beer.name.ToUpper().Contains(p.ToUpper())) select beer).ToList());
        }

        public void Clear()
        {
            list.Clear();
            if (blwm != null)
                blwm.list = new ObservableCollection<Beer>(list);
        }

        public void Load(Newtonsoft.Json.Linq.JArray jArray)
        {
            Beer newBeer;
            foreach (JObject obj in jArray)
            {
                newBeer = new Beer();
                newBeer.load(obj);
                Add(newBeer);
            }
        }

        public void Load(JObject objFavBeer)
        {
            try
            {
                JArray array = objFavBeer["beers"].ToObject<JArray>();
                Load(array);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void setFav(BeerList beerlist)
        {
            foreach (Beer b in beerlist.list)
            {
                b.isFav = false;
                foreach (Beer bT in list)
                {
                    if (bT.id == b.id)
                        b.isFav = true;
                }
            }
            
        }

        public void setFav()
        {
            foreach (Beer b in list)
            {
                b.isFav = true;
            }
        }
    }
}
