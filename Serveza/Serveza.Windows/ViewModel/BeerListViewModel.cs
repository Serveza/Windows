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
    public class BeerListViewModel : ViewModelBase
    {
        private BeerListView _beerListView;
        private ObservableCollection<Beer> _list;
        private CoreDispatcher dispatcher;
        public ObservableCollection<Beer> list
        {
            get { return _list; }
            set
            {
                _list = value;
                update();
            }
        }

        public BeerListViewModel(Grid mainGrid)
        {
            list = new ObservableCollection<Beer>();
            _beerListView = new BeerListView();
            _beerListView.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(_beerListView);
        }

        private async void update()
        {
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _beerListView.listViewP.DataContext = _list;
            });
        }
    }
}
