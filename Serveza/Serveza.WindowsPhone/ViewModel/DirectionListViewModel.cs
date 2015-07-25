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
    public class DirectionListViewModel : ViewModelBase
    {
        private DirectionListView _beerListView;
        private ObservableCollection<Direction> _list;
        private CoreDispatcher dispatcher;
        public ObservableCollection<Direction> list
        {
            get { return _list; }
            set
            {
                _list = value;
                update();
            }
        }

        public DirectionListViewModel(Grid mainGrid)
        {
            list = new ObservableCollection<Direction>();
            _beerListView = new DirectionListView();
            _beerListView.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(_beerListView);
            //_beerListView.listViewP.SelectionChanged += listViewP_SelectionChanged;
        }

        public void SetTimeAndDist(double time, double dist)
        {
            _beerListView.setTimeAndDist(time, dist);
        }

        private async void update()
        {
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _beerListView.pListView.DataContext = list;
            });
        }
    }
}
