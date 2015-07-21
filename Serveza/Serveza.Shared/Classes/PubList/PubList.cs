﻿using Newtonsoft.Json.Linq;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

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
                foreach (JObject bar in array)
                {
                    name = bar["name"].ToObject<string>();
                    posString = bar["position"].ToObject<string>();
                    url = bar["url"].ToObject<string>();

                    posStringSplit = posString.Split(' ', ',');
                    this.Add(new Pub(name, Convert.ToDouble(posStringSplit[0]), Convert.ToDouble(posStringSplit[2]), url));
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public void Add(Pub newPub)
        {
            list.Add(newPub);
            if (plvm != null)
                plvm.list = new ObservableCollection<Pub>(list);
            try
            {
                newPub.getInfo();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Clear()
        {
            list.Clear();
            if (plvm != null)
                plvm.list = new ObservableCollection<Pub>(list);
        }
    }
}