using Serveza.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Network
{
    /*
     * {
  "notifications": [
    {
      "bar": "/api/bars/1",
      "created_at": "Wed, 22 Jul 2015 20:50:25 GMT",
      "description": null,
      "end": null,
      "location": null,
      "name": "Titi",
      "start": null,
      "type": "bar_event"
    },
    {
      "bar": "/api/bars/1",
      "created_at": "Wed, 22 Jul 2015 20:50:25 GMT",
      "description": null,
      "end": null,
      "location": null,
      "name": "Toto",
      "start": null,
      "type": "bar_event"
    }
  ]
}
     */
    public enum EventType
    {
        BAR,
        NOTIF,
        UPDATE,
        NONE,
    }

    public class GetEvent : Request
    {
        public GetEvent() :
            base("/api/user/notifications", RequestType.GET)
        {

        }


        public void SetParam(string token, PubList.PubList list)
        {
            url += "?api_token=" + token;
            foreach (Pub p in list.list)
            {
                url += "&bar=" + p.id;
            }
            uri = new Uri(url);
        }

        public void SetParam(string token, Pub pub)
        {
            url += "?api_token=" + token;
            url += "&bar=" + pub.id;
            uri = new Uri(url);
        }
        public void SetParam(string token, EventType type, bool update = false)
        {
            switch (type)
            {
                case (EventType.BAR) :
                    break;
                case(EventType.NOTIF) :
                    break;
                case (EventType.UPDATE):
                    break;
                case(EventType.NONE):
                    url += "?api_token=" + token;
                    break;
            }
            if (update)
                url += "&update=true";

            uri = new Uri(url);
        }
    }
}
