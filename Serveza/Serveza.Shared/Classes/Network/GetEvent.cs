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
        NONE,
    }

    public class GetEvent : Request
    {
        public GetEvent() :
            base("/api/user/notifications", RequestType.GET)
        {

        }

        public void SetParam(string token, EventType type)
        {
            switch (type)
            {
                case (EventType.BAR) :
                    break;
                case(EventType.NOTIF) :
                    break;
                case(EventType.NONE):
                    url += "?api_token=" + token;
                    break;
            }

            uri = new Uri(url);
        }
    }
}
