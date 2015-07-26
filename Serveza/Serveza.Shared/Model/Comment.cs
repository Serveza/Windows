using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Model
{
    public class Comment
    {
        public string text { get; set; }
        public string userName { get; set; }
        public int note { get; set; }
        public string image { get; set; }
        public Comment(string Text, string UserName, int Note)
        {
            text = Text;
            userName = UserName;
            note = Note;
        }

        public Comment()
        {
            
        }

        public void Load(JObject obj)
        {
            try 
            {
                JObject autor = obj["author"].ToObject<JObject>();
                userName = (autor["firstname"].ToObject<string>() == null ? "no user" : autor["firstname"].ToObject<string>());
                image = (autor["avatar"].ToObject<string>() == null ? "no user" : autor["firstname"].ToObject<string>());
                text = obj["comment"].ToObject<string>();
                note = obj["score"].ToObject<int>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
