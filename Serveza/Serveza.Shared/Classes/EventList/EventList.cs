using Newtonsoft.Json.Linq;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.EventList
{
    public class EventList
    {
        public List<Event> list;
        private EventListViewModel _elvm;
        public EventListViewModel elvm
        {
            get { return _elvm; }
            set
            {
                _elvm = value;
                if (_elvm != null)
                    _elvm.list = new ObservableCollection<Event>(list);
            }
        }

        public EventList(EventListViewModel elvm)
        {
            list = new List<Event>();
            this.elvm = elvm;
        }

        public EventList()
        {
            list = new List<Event>();
        }

        public void Add(Event newEvent)
        {
            list.Add(newEvent);
            if (elvm != null)
                elvm.list = new ObservableCollection<Event>(list);
        }


        public void LoadEvent(JObject jObject)
        {
            Debug.WriteLine(jObject);
            try
            {
                Event eventTmp;
                JArray array = jObject["notifications"].ToObject<JArray>();
                foreach (JObject obj in array)
                {
                    eventTmp = new Event();
                    eventTmp.Load(obj);
                    Add(eventTmp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
