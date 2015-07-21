using Serveza.Model;
using Serveza.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Serveza.ViewModel
{
    public class CommentListViewModel : ViewModelBase
    {
        private CommentListView _commentListView;
        private ObservableCollection<Comment> _list;
        private CoreDispatcher dispatcher;
        public ObservableCollection<Comment> list
        {
            get { return _list; }
            set
            {
                _list = value;
                update();
            }
        }

        public CommentListViewModel(Grid mainGrid)
        {
            list = new ObservableCollection<Comment>();
            _commentListView = new CommentListView();
            _commentListView.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(_commentListView);

        }

        private async void update()
        {
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _commentListView.CommentList.DataContext = _list;
            });
        }
    }
}
