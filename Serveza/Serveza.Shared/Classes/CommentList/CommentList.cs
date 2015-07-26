using Newtonsoft.Json.Linq;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Classes.CommentList
{
    public class CommentList
    {
        public List<Comment> list;
        public int moyen { get; private set; }

        private CommentListViewModel _clvm;

        public CommentListViewModel clvm
        {
            get {return _clvm;}
            set
            {
                _clvm = value;
                if (_clvm != null)
                    _clvm.list = new System.Collections.ObjectModel.ObservableCollection<Comment>(list);
            }
        }
         public CommentList()
        {
            list = new List<Comment>();
            moyen = 0;
        }
        public CommentList(CommentListViewModel clvm)
        {
            list = new List<Comment>();
            _clvm = clvm;
            moyen = 0;
        }

        public void Add(Comment newComment)
        {
            list.Add(newComment);
            moyen = (int)((newComment.note + moyen) / list.Count);
            if (_clvm != null)
                _clvm.list = new System.Collections.ObjectModel.ObservableCollection<Comment>(list);
        }

        public void Load(JObject obj)
        {
            try
            {
                Debug.WriteLine(obj);
                JArray array = obj["comments"].ToObject<JArray>();
                Comment tmp;
                foreach (JObject objt in array)
                {
                    tmp = new Comment();
                    tmp.Load(objt);
                    Add(tmp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
