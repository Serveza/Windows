using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.CommentList
{
    public class CommentList
    {
        public List<Comment> list;

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
        }
        public CommentList(CommentListViewModel clvm)
        {
            list = new List<Comment>();
            _clvm = clvm;
        }

        public void Add(Comment newComment)
        {
            list.Add(newComment);
            if (_clvm != null)
                _clvm.list = new System.Collections.ObjectModel.ObservableCollection<Comment>(list);
        }
    }
}
