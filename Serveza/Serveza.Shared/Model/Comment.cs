using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Model
{
    public class Comment
    {
        public string text { get; set; }
        public string userName { get; set; }
        public int note { get; set; }

        public Comment(string Text, string UserName, int Note)
        {
            text = Text;
            userName = UserName;
            note = Note;
        }
    }
}
