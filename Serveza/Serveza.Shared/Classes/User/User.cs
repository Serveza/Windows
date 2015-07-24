using Newtonsoft.Json.Linq;
using Serveza.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.User
{
    public class User
    {
        private string _firstName;
        private string _lastName;

        public EventList.EventList eventList;
        public string imageUrl { get; set; }
        public string firstName { get { return _firstName; } set { _firstName = value; } }
        public string lastName { get { return _lastName; } set { _lastName = value; } }
        public string name
        {
            get
            {
                return _firstName + " " + _lastName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string mail { get; set; }

        public User()
        {
            eventList = new EventList.EventList();
        }

        public bool LoadEvent(JObject obj)
        {

            return true;
        }
        public bool Load(JObject obj)
        {
            Debug.WriteLine("ok");
            Debug.WriteLine(obj);
            try
            {
                firstName = obj["firstname"].ToObject<string>();
                lastName = obj["lastname"].ToObject<string>();
                imageUrl = obj["avatar"].ToObject<string>();
                App.Core.netWork.token = obj["api_token"].ToObject<string>();
                Debug.WriteLine("done");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool LoadNotification(JObject obj)
        {
            try
            {
                JArray NotificationArray = obj["notifications"].ToObject<JArray>();
                Event newEvent;
                foreach (JObject notif in NotificationArray)
                {
                    newEvent = new Event();
                    newEvent.Load(notif);
                    eventList.Add(newEvent);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

        }


        public void LoadEvent(System.Threading.Tasks.Task<JObject> task)
        {
            JObject obj = task.Result as JObject;
            LoadEvent(obj);
        }

        public bool Load(System.Threading.Tasks.Task<JObject> task)
        {
            Debug.WriteLine("load");
            JObject obj = task.Result as JObject;
            Debug.WriteLine("obj = " + obj.ToString());
            if (Load(obj))
                Utils.NavigationControl.Navigate(typeof(Pages.HomePage));
            return true;
        }
    }
}
